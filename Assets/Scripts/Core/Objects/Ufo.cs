using Core.Config;
using Core.Interface.Objects;
using UnityEngine;

namespace Core.Objects {
    public class Ufo : Enemy<UfoConfig>, IUfo {
        public AudioSource normalAudio;
        public AudioSource huntAudio;

        private bool huntState;

        public override float Radius => config.colliderRadius;

        private Transform target;
        private float huntCountdown;

        public delegate void ExplosionEvent();
        public static event ExplosionEvent Explosion;

        public void SetTarget(Transform target) {
            this.target = target;
            huntCountdown = config.huntDelay;
        }

        public void Hunt() {
            normalAudio.Stop();
            huntAudio.Play();
        }

        public override void Reset() {
            base.Reset();
            huntAudio.Stop();
            target = null;
            huntState = default;
        }

        public override void Destroy() {
            base.Destroy();
            Explosion?.Invoke();
        }


        private void Update() {
            if ((huntCountdown -= Time.deltaTime) > 0) {
                transform.Translate(direction * (config.startSpeed * Time.deltaTime));
            } else {
                if (!huntState) {
                    huntState = true;
                    Hunt();
                }
                Vector3 huntDirection = -(transform.position - target.position).normalized;
                transform.Translate(huntDirection * (config.huntSpeed * Time.deltaTime));
            }
        }

    }
}