using Core.Base;
using Core.Config;
using Core.Objects;
using Core.Pools.Base;
using Core.State;
using UnityEngine;

namespace Core.Pools {
    public class BulletPool : EntityPool<Bullet, BulletAmmoState, BulletConfig> {

        public BulletPool(GameObject prefab, BulletConfig config) : base(prefab, config) { }

    }
}