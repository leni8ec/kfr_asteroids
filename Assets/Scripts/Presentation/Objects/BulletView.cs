using Core.Config;
using Core.Objects;
using Core.State;
using Presentation.Objects.Base;

namespace Presentation.Objects {
    public class BulletView : EntityView<Bullet, BulletAmmoState, BulletConfig> {

        private void Start() {
            Entity.FireEvent += FireHandle;

        }

        private void FireHandle() { }

    }
}