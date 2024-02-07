using Model.Core.Config;
using Model.Core.Objects;
using Model.Core.State;
using UnityEngine;
using UnityView.Objects.Base;

namespace UnityView.Objects {
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