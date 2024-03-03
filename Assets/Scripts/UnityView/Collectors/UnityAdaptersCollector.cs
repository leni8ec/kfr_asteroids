using Model.Adapters;
using Presenter.Collectors;
using Presenter.Collectors.Base;
using UnityEngine;
using UnityView.Adapters;

namespace UnityView.Collectors {
    public class UnityAdaptersCollector : MonoBehaviour, ICollectorCreator<AdaptersCollector> {

        [SerializeField] private new UnityCameraAdapter camera;
        [SerializeField] private new UnityAudioAdapter audio;

        public AdaptersCollector CreateCollector() {
            AdaptersCollector collector = new();

            // => Use concrete adapter interfaces explicitly!
            collector.Add<ICameraAdapter>(camera);
            collector.Add<IAudioAdapter>(audio);

            return collector;
        }

    }
}