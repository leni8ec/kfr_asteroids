using Core.Base;
using Core.Config;
using Core.Objects;
using Core.Pools.Base;
using Core.State;
using UnityEngine;

namespace Core.Pools {
    public class LaserPool : EntityPool<Laser, LaserAmmoState, LaserConfig> {

        public LaserPool(GameObject prefab, LaserConfig config) : base(prefab, config) { }

    }
}