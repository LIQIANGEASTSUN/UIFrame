using UnityEngine;

namespace UIFrame
{
    public interface IUIView
    {
        void Open(Transform tr, IUIController controller);
    }
}