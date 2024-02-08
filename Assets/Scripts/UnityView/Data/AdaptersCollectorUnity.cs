using Model.Core.Adapters;
using UnityEngine;
using UnityView.Adapters;

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