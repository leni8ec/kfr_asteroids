using Core.Config;
using Core.Objects;
using Core.State;
using Presentation.Objects.Base;

namespace Presentation.Objects {
    public class BulletView : EntityView<Bullet, BulletAmmoState, BulletConfig> {

        protected override void SubscribeEvents() {
            Entity.FireEvent += FireHandle;
        }

        private void FireHandle() { }

    }
}