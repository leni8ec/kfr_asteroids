using Model.Core.Data.State;
using Model.Core.Objects.Game;
using Model.Core.Pools.Base;
using Model.Core.Unity.Data.Config;
using UnityEngine;

namespace Model.Core.Pools {
    public class BulletPool : EntityPool<Bullet, BulletAmmoState, BulletConfig> {

        public BulletPool(GameObject prefab, BulletConfig config) : base(prefab, config) { }
    }
}