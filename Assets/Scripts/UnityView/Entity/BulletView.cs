using Model.Data.State;
using Model.Data.Unity.Config;
using Model.Entity;
using UnityView.Entity.Base;

namespace UnityView.Entity {
    public class BulletView : EntityView<Bullet, BulletAmmoState, BulletConfig> {

        protected override void SubscribeEvents() {
            Entity.FireEvent += FireHandle;
        }

        private void FireHandle() { }

    }
}