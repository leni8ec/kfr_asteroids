using Model.Core.Adapters;
using UnityEngine;
using UnityView.Objects.World;

namespace UnityView.Data {
    public class AdaptersCollectorUnity : MonoBehaviour {

        public new CameraAdapter camera;

        public AdaptersCollector CreateCollector() {
            AdaptersCollector collector = new() {
                camera = camera
            };

            return collector;
        }

    }
}