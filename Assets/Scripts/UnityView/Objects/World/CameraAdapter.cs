using Model.Core.Interface.Adapters;
using UnityEngine;
using UnityView.Base;

namespace UnityView.Objects.World {
    public class CameraAdapter : MonoBase, ICameraAdapter {

        [SerializeField] private Camera mainCamera;

        public Vector3 ScreenToWorldPoint(Vector3 screenPoint) {
            return mainCamera.ScreenToWorldPoint(screenPoint);
        }

        public float ScreenWidth => Screen.width;
        public float ScreenHeight => Screen.height;

    }
}