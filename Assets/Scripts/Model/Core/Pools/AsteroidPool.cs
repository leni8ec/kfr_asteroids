using Model.Core.Config;
using Model.Core.Objects;
using Model.Core.Pools.Base;
using Model.Core.State;
using UnityEngine;

namespace Model.Core.Pools {
    public class AsteroidPool : EntityPool<Asteroid, AsteroidState, AsteroidConfig> {

        public AsteroidPool(GameObject prefab, AsteroidConfig config) : base(prefab, config) { }
    }
}