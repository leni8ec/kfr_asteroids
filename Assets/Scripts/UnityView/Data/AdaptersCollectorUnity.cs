using Model.Core.Adapters;
using UnityEngine;
using UnityView.Adapters;

namespace UnityView.Data {
    public class AdaptersCollectorUnity : MonoBehaviour {

        public new UnityCameraAdapter camera;
        public new UnityAudioAdapter audio;

        public AdaptersCollector CreateCollector() {
            AdaptersCollector collector = new() {
                camera = camera,
                audio = audio
            };

            return collector;
        }

    }
}