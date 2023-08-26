using UnityEngine;

namespace UIFrame
{
    public struct UIPlaneGoLoad
    {
        private UIConfig _uiConfig;
        private Transform _layerTr;
        private IUIDataBase _data;
        public UIPlaneGoLoad(UIConfig uiConfig, Transform layerTr, IUIDataBase data)
        {
            _uiConfig = uiConfig;
            _layerTr = layerTr;
            _data = data;

            ResourceRequest resourceRequest = Resources.LoadAsync<GameObject>(uiConfig.AssetName);
            resourceRequest.completed += LoadComplete;
        }

        private void LoadComplete(AsyncOperation asyncOperation)
        {
            ResourceRequest resourceRequest = asyncOperation as ResourceRequest;
            GameObject g = resourceRequest.asset as GameObject;
            GameObject instance = GameObject.Instantiate(g, _layerTr);
            instance.transform.SetAsLastSibling();
            instance.transform.localScale = Vector3.one;
            instance.transform.rotation = Quaternion.identity;
            instance.transform.localPosition = Vector3.zero;
            _uiConfig.Plane.SetTransform(instance.transform);
            _uiConfig.Plane.Open(_data);
        }
    }
}