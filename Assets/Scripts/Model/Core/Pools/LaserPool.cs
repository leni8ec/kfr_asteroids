using Model.Core.Data.State;
using Model.Core.Objects.Game;
using Model.Core.Pools.Base;
using Model.Core.Unity.Data.Config;
using UnityEngine;

namespace Model.Core.Pools {
    public class LaserPool : EntityPool<Laser, LaserAmmoState, LaserConfig> {

        public LaserPool(GameObject prefab, LaserConfig config) : base(prefab, config) { }
    }
}