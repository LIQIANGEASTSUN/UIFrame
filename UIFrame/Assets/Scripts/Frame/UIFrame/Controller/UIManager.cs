using System.Collections.Generic;
using UnityEngine;

namespace UIFrame
{
    public class UIManager : SingletonObject<UIManager>
    {
        private UIOpenPlaneController _openPlaneController;
        private UIConfigController _uiConfigController;

        private Transform _root;
        private Dictionary<string, Transform> _layerDic;

        public UIManager()
        {
            _openPlaneController = new UIOpenPlaneController();
            _uiConfigController = new UIConfigController();
            _root = GameObject.Find("UIRoot").transform;
            _layerDic = new Dictionary<string, Transform>();
        }

        /// <summary>
        /// 打开界面
        /// 原则：同一个界面同时只存在一个
        /// 如果要打开的界面已经打开了，例：打开界面 A，目前打开的界面顺
        /// 序为 A-B-C-D，则依次从栈中取出 D、C、B 并关闭，然后刷新界面A
        /// 为什么？避免出现 A-B-C-D-A-B-C-D-A-B-C-D 此类无限循环的界面
        /// </summary>
        /// <param name="type">界面枚举值</param>
        /// <param name="data">需要传递给界面的数据，需要是 IUIDataBase 类型</param>
        public void Open(UIPlaneType type, IUIDataBase data)
        {
            UIConfig info = null;
            if (IsOpen(type))
            {
                UIConfig lastInfo = _openPlaneController.LastOpenPlaneInfo();
                while ((null != lastInfo) && lastInfo.Type != type)
                {
                    Close(lastInfo.Type);
                    lastInfo = _openPlaneController.LastOpenPlaneInfo();
                }

                info = _uiConfigController.GetConfig(type);
                info.Plane.Tr.gameObject.SetActive(true);
                Resume(info);
                if (info.Plane.LoadComplete())
                {
                    info.Plane.Open(data);
                }
            }
            else
            {
                UIConfig last = _openPlaneController.LastOpenPlaneInfo();
                Hungup(last);
                info = _uiConfigController.GetConfig(type);
                LoadPanel(info, data);
                info.Plane.Init(type);
                _openPlaneController.AddInfo(info);
            }
        }

        public void Close(UIPlaneType type)
        {
            UIConfig info = _uiConfigController.GetConfig(type);
            if (null != info)
            {
                info.Plane.Close();
                info.Plane.Tr.gameObject.SetActive(false);
                _openPlaneController.RemoveInfo(info);
                info.Plane.Destroy();
                GameObject.Destroy(info.Plane.Tr.gameObject);
            }

            info = _openPlaneController.LastOpenPlaneInfo();
            Resume(info);
        }

        // 挂起面板，打开一个面板挂起最后一个面板
        private void Hungup(UIConfig info)
        {
            if (null != info && !info.Plane.IsHungUp)
            {
                info.Plane.HangUp();
            }
        }

        private void Resume(UIConfig info)
        {
            if (null != info && info.Plane.IsHungUp)
            {
                info.Plane.Resume();
            }
        }

        /// <summary>
        /// 返回上一个界面：关闭最后一个打开的界面
        /// </summary>
        public void Back()
        {
            UIConfig info = _openPlaneController.LastOpenPlaneInfo();
            if (null != info)
            {
                Close(info.Type);
            }
        }

        public bool IsOpen(UIPlaneType type)
        {
            return _openPlaneController.IsOpen(type);
        }

        private void LoadPanel(UIConfig uiConfig, IUIDataBase data)
        {
            Transform layerTr = LayerTransform(uiConfig.Layer);
            UIPlaneGoLoad uIPlaneGoLoad = new UIPlaneGoLoad(uiConfig, layerTr, data);
        }

        private Transform LayerTransform(string layerName)
        {
            Transform tr = null;
            if (!_layerDic.TryGetValue(layerName, out tr))
            {
                tr = _root.Find(layerName);
                _layerDic[layerName] = tr;
            }
            return tr;
        }

        public UIConfigController UIConfigController
        {
            get
            {
                return _uiConfigController;
            }
        }
    }
}
