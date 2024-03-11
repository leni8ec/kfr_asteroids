using System;
using Model.Data.State;
using Model.Data.Unity.Config;
using Model.Entity.Base;
using Model.Entity.Interface;
using UnityEngine;

namespace Model.Entity {
    public class Ufo : Enemy<UfoState, UfoConfig>, IUfo {

        public event Action HuntEvent;
        public event Action ResetEvent;
        public static event Action ExplosionEvent;


        protected override void OnReset() {
            ResetEvent?.Invoke();
        }

        protected override void OnDestroy() {
            ExplosionEvent?.Invoke();
        }

        public void SetTarget(EntityBase target) {
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
                Vector3 huntDirection = -(Transform.position - State.target.Position).normalized;
                Transform.Translate(huntDirection * (Config.huntSpeed * deltaTime));
            }
        }
    }
}