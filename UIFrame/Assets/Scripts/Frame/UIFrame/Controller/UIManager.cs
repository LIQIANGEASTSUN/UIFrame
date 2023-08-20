using System.Collections.Generic;
using UnityEngine;

namespace UIFrame
{
    public class UIManager : SingletonObject<UIManager>
    {
        private UIInfoController _uiInfoController;
        private UIConfigController _uiConfigController;

        private Transform _root;
        private Dictionary<string, Transform> _layerDic;

        public UIManager()
        {
            _uiInfoController = new UIInfoController();
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
        public void Open(UIPlaneType type, IUIDataBase data)
        {
            UIPlaneInfo info = null;
            if (IsOpen(type))
            {
                info = _uiInfoController.GetOpenPlaneInfo(type);
                UIPlaneInfo lastInfo = _uiInfoController.LastOpenPlaneInfo();
                while ((null != lastInfo) && lastInfo.Type != type)
                {
                    Close(lastInfo.Type);
                    lastInfo = _uiInfoController.LastOpenPlaneInfo();
                }
            }

            Open(info, type, data);
        }

        private void Open(UIPlaneInfo info, UIPlaneType type, IUIDataBase data)
        {
            if (null == info)
            {
                info = _uiInfoController.GetRecyclePlaneInfo(type);
            }

            if (null != info)
            {
                info.Plane.Tr.gameObject.SetActive(true);
                if (info.Plane.LoadComplete())
                {
                    info.Plane.Open(data);
                }
            }
            else
            {
                UIBasePlane plane = LoadPanel(type, data);
                plane.Init(type);
                info = new UIPlaneInfo(type, plane);
            }

            Hungup(type);
            info.IsRecycle = false;
            if (!IsOpen(info.Type))
            {
                _uiInfoController.AddInfo(info);
            }

            _uiInfoController.Update();
        }

        // 挂起面板，打开一个面板挂起最后一个面板
        private void Hungup(UIPlaneType type)
        {
            UIPlaneInfo info = _uiInfoController.LastOpenPlaneInfo();
            if (null != info && info.Type != type && !info.Plane.IsHungUp)
            {
                info.Plane.HangUp();
            }
        }

        public void Close(UIPlaneType type)
        {
            UIPlaneInfo info = _uiInfoController.GetOpenPlaneInfo(type);
            if (null != info)
            {
                info.IsRecycle = true;
                info.RecycleTime = (int)Time.realtimeSinceStartup;
                info.Plane.Close();
                info.Plane.Tr.gameObject.SetActive(false);
                _uiInfoController.RemoveInfo(info);
            }

            Resume();
            _uiInfoController.Update();
        }

        /// <summary>
        /// 返回上一个界面
        /// 关闭最有一个打开的界面
        /// </summary>
        public void Back()
        {
            UIPlaneInfo info = _uiInfoController.LastOpenPlaneInfo();
            if (null != info)
            {
                Close(info.Type);
            }
        }

        private void Resume()
        {
            UIPlaneInfo info = _uiInfoController.LastOpenPlaneInfo();
            if (null != info && info.Plane.IsHungUp)
            {
                info.Plane.Resume();
            }
        }

        public bool IsOpen(UIPlaneType type)
        {
            UIPlaneInfo info = _uiInfoController.GetOpenPlaneInfo(type);
            return null != info;
        }

        private UIBasePlane LoadPanel(UIPlaneType type, IUIDataBase data)
        {
            UIConfig uiConfig = _uiConfigController.GetConfig(type);
            if (null == uiConfig.BasePlane.Tr)
            {
                Transform layerTr = LayerTransform(uiConfig.Layer);
                UIPlaneGoLoad uIPlaneGoLoad = new UIPlaneGoLoad(uiConfig, layerTr, data);
            }

            return uiConfig.BasePlane;
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
    }
}
