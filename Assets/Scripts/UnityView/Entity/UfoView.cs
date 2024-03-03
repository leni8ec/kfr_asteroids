using Model.Data.State;
using Model.Data.Unity.Config;
using Model.Entity;
using UnityEngine;
using UnityView.Entity.Base;

namespace UnityView.Entity {
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