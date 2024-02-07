using Model.Core.Config;
using Model.Core.Objects;
using Model.Core.Pools.Base;
using Model.Core.State;
using UnityEngine;

namespace Model.Core.Pools {
    public class LaserPool : EntityPool<Laser, LaserAmmoState, LaserConfig> {

        public LaserPool(GameObject prefab, LaserConfig config) : base(prefab, config) { }
    }
}