using Model.Core.Data.State;
using Model.Core.Objects.Game;
using Model.Core.Pools.Base;
using Model.Core.Unity.Data.Config;
using UnityEngine;

namespace Model.Core.Pools {
    public class AsteroidPool : EntityPool<Asteroid, AsteroidState, AsteroidConfig> {

        public AsteroidPool(GameObject prefab, AsteroidConfig config) : base(prefab, config) { }
    }
}