using System;
using Core.Config;
using Core.Interface.Objects;
using Core.Objects.Base;
using Core.State;
using UnityEngine;

namespace Core.Objects {
    public class Ufo : Enemy<UfoState, UfoConfig>, IUfo {

        public event Action HuntEvent;
        public event Action ResetEvent;
        public static event Action ExplosionEvent;

        protected override void OnReset() {
            ResetEvent?.Invoke();
        }

        public override void Destroy() {
            base.Destroy();
            ExplosionEvent?.Invoke();
        }

        protected override void Initialize() { }

        public void SetTarget(Transform target) {
            State.target = target;
            State.huntCountdown = Config.huntDelay;
        }

        public void Hunt() {
            HuntEvent?.Invoke();
        }

        public override void Upd(float deltaTime) {
            if ((State.huntCountdown -= Time.deltaTime) > 0) {
                Transform.Translate(State.Direction * (Config.startSpeed * deltaTime));
            } else {
                if (!State.huntState) {
                    State.huntState = true;
                    Hunt();
                }
                Vector3 huntDirection = -(Transform.position - State.target.position).normalized;
                Transform.Translate(huntDirection * (Config.huntSpeed * deltaTime));
            }
        }
    }
}