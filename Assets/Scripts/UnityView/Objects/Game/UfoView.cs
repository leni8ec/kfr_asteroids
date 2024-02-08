using Model.Core.Data.State;
using Model.Core.Objects.Game;
using Model.Core.Unity.Data.Config;
using UnityEngine;
using UnityView.Objects.Game.Base;

namespace UnityView.Objects.Game {
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