using Model.Core.Data.State;
using Model.Core.Objects;
using Model.Core.Unity.Data.Config;
using UnityView.Objects.Base;

namespace UnityView.Objects {
    public class BulletView : EntityView<Bullet, BulletAmmoState, BulletConfig> {

        protected override void SubscribeEvents() {
            Entity.FireEvent += FireHandle;
        }

        private void FireHandle() { }

    }
}