using Model.Core.Config;
using Model.Core.Objects;
using Model.Core.Pools.Base;
using Model.Core.State;
using UnityEngine;

namespace Model.Core.Pools {
    public class BulletPool : EntityPool<Bullet, BulletAmmoState, BulletConfig> {

        public BulletPool(GameObject prefab, BulletConfig config) : base(prefab, config) { }
    }
}