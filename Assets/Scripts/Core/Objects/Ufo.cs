using Core.Config;
using Core.Interface.Objects;
using Core.Objects.Base;
using Core.State;
using UnityEngine;

namespace Core.Objects {
    public class Ufo : Enemy<UfoState, UfoConfig>, IUfo {
        [SerializeField] private AudioSource normalAudio;
        [SerializeField] private AudioSource huntAudio;

        public delegate void ExplosionEvent();
        public static event ExplosionEvent Explosion;

        protected override void Initialize() { }

        public override void Reset() {
            huntAudio.Stop();
        }

        public override void Destroy() {
            base.Destroy();
            Explosion?.Invoke();
        }


        // todo: move to state
        public void SetTarget(Transform target) {
            State.target = target;
            State.huntCountdown = Config.huntDelay;
        }

        public void Hunt() {
            normalAudio.Stop();
            huntAudio.Play();
        }

        private void Update() {
            if ((State.huntCountdown -= Time.deltaTime) > 0) {
                Transform.Translate(State.Direction * (Config.startSpeed * Time.deltaTime));
            } else {
                if (!State.huntState) {
                    State.huntState = true;
                    Hunt();
                }
                Vector3 huntDirection = -(Transform.position - State.target.position).normalized;
                Transform.Translate(huntDirection * (Config.huntSpeed * Time.deltaTime));
            }
        }

    }
}