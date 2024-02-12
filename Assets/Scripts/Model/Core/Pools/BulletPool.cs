using Model.Core.Data.State;
using Model.Core.Entity;
using Model.Core.Pools.Base;
using Model.Core.Unity.Data.Config;

namespace Model.Core.Pools {
    public class BulletPool : EntityPool<Bullet, BulletAmmoState, BulletConfig> {

        public BulletPool(BulletConfig config) : base(new EntityFactory<Bullet, BulletAmmoState, BulletConfig>(config)) { }

    }
}