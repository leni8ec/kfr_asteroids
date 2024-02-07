using Core.Config;
using Core.Objects;
using Core.State;
using Presentation.Objects.Base;
using UnityEngine;

namespace Presentation.Objects {
    public class UfoView : EntityView<Ufo, UfoState, UfoConfig> {
        [SerializeField] private AudioSource normalAudio;
        [SerializeField] private AudioSource huntAudio;

        protected override void SubscribeEvents() {
            Entity.HuntEvent += HuntHandler;
            Entity.ResetEvent += ResetHandler;
        }


        private void ResetHandler() {
            huntAudio.Stop();
        }

        private void HuntHandler() {
            normalAudio.Stop();
            huntAudio.Play();
        }

    }
}