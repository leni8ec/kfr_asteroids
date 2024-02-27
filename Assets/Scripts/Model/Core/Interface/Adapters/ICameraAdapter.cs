using Model.Core.Interface.Adapters.Base;
using UnityEngine;

namespace Model.Core.Interface.Adapters {
    public interface ICameraAdapter : IAdapter {

        Vector3 ScreenToWorldPoint(Vector3 screenPoint);
        Rect GetWorldLimits(float screenOffset);

        float ScreenWidth { get; }
        float ScreenHeight { get; }

    }
}