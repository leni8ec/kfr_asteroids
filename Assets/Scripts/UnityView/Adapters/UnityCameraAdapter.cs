using Model.Adapters;
using UnityEngine;
using UnityView.Base;

namespace UnityView.Adapters {
    public class UnityCameraAdapter : MonoBase, ICameraAdapter {

        [SerializeField] private Camera mainCamera;

        public float ScreenWidth => Screen.width;
        public float ScreenHeight => Screen.height;

        public Vector3 ScreenToWorldPoint(Vector3 screenPoint) {
            return mainCamera.ScreenToWorldPoint(screenPoint);
        }

        public Rect GetWorldLimits(float screenOffset) {
            Vector2 min = ScreenToWorldPoint(Vector3.zero);
            Vector2 max = ScreenToWorldPoint(new Vector3(ScreenWidth, ScreenHeight));
            Rect limits = new(min.x - screenOffset, min.y - screenOffset, max.x - min.x + screenOffset * 2, max.y - min.y + screenOffset * 2);
            return limits;
        }


    }
}