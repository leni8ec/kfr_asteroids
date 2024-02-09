using Model.Core.Data.State;
using Model.Core.Entity;
using Model.Core.Pools.Base;
using Model.Core.Unity.Data.Config;

namespace Model.Core.Pools {
    public class LaserPool : EntityPool<Laser, LaserAmmoState, LaserConfig> {

        public LaserPool(LaserConfig config) : base(config) { }
    }
}