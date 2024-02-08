using Model.Core.Data.State;
using Model.Core.Objects.Game;
using Model.Core.Unity.Data.Config;
using UnityView.Objects.Game.Base;

namespace UnityView.Objects.Game {
    public class BulletView : EntityView<Bullet, BulletAmmoState, BulletConfig> {

        protected override void SubscribeEvents() {
            Entity.FireEvent += FireHandle;
        }

        private void FireHandle() { }

    }
}