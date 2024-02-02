using Core.Config;
using Core.Interface.Objects;
using UnityEngine;

namespace Core.Objects {
    public class Ufo : Enemy<UfoConfig>, IUfo {
        public override float Radius => data.colliderRadius;

        private Transform target;
        private float huntCountdown;

        public delegate void ExplosionEvent();
        public static event ExplosionEvent Explosion;

        public void SetTarget(Transform target) {
            this.target = target;
            huntCountdown = data.huntDelay;
        }

        public void Hunt() { }

        public override void Reset() {
            base.Reset();
            target = null;
        }

        public override void Destroy() {
            base.Destroy();
            Explosion?.Invoke();
        }


        private void Update() {
            if ((huntCountdown -= Time.deltaTime) > 0) {
                transform.Translate(direction * (data.startSpeed * Time.deltaTime));
            } else {
                Vector3 huntDirection = -(transform.position - target.position).normalized;
                transform.Translate(huntDirection * (data.huntSpeed * Time.deltaTime));
            }
        }

    }
}