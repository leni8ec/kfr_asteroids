using Model.Core.Interface.Adapters;
using UnityEngine;
using UnityView.Base;

namespace UnityView.Adapters {
    // todo: rename to UnityCameraAdapter
    public class UnityCameraAdapter : MonoBase, ICameraAdapter {

        [SerializeField] private Camera mainCamera;

        public Vector3 ScreenToWorldPoint(Vector3 screenPoint) {
            return mainCamera.ScreenToWorldPoint(screenPoint);
        }

        public float ScreenWidth => Screen.width;
        public float ScreenHeight => Screen.height;

    }
}