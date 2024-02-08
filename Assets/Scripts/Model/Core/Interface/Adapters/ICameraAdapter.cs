using UnityEngine;

namespace Model.Core.Interface.Adapters {
    public interface ICameraAdapter {

        Vector3 ScreenToWorldPoint(Vector3 screenPoint);

        float ScreenWidth { get; }
        float ScreenHeight { get; }

    }
}