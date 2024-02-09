using Model.Core.Data.State;
using Model.Core.Entity;
using Model.Core.Unity.Data.Config;
using UnityView.Entity.Base;

namespace UnityView.Entity {
    public class BulletView : EntityView<Bullet, BulletAmmoState, BulletConfig> {

        protected override void SubscribeEvents() {
            Entity.FireEvent += FireHandle;
        }

        private void FireHandle() { }

    }
}