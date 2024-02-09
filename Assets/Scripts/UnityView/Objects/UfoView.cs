using Model.Core.Data.State;
using Model.Core.Objects;
using Model.Core.Unity.Data.Config;
using UnityEngine;
using UnityView.Objects.Base;

namespace UnityView.Objects {
    public class UfoView : EntityView<Ufo, UfoState, UfoConfig> {
        [SerializeField] private AudioSource normalAudio;
        [SerializeField] private AudioSource huntAudio;

        protected override void SubscribeEvents() {
            Entity.HuntEvent += HuntHandler;
            Entity.DestroyEvent += DestroyHandler;
        }

        private void DestroyHandler() {
            huntAudio.Stop();
        }

        private void HuntHandler() {
            normalAudio.Stop();
            huntAudio.Play();
        }

    }
}