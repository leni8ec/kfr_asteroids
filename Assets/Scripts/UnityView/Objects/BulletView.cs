using Model.Core.Config;
using Model.Core.Objects;
using Model.Core.State;
using UnityView.Objects.Base;

namespace UnityView.Objects {
    public class BulletView : EntityView<Bullet, BulletAmmoState, BulletConfig> {

        protected override void SubscribeEvents() {
            Entity.FireEvent += FireHandle;
        }

        private void FireHandle() { }

    }
}