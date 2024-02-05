using Core.Base;
using Core.Config;
using Core.Objects;
using Core.State;
using UnityEngine;

namespace Core.Pools {
    public class AsteroidPool : EntityPool<Asteroid, AsteroidState, AsteroidConfig> {

        public AsteroidPool(GameObject prefab, AsteroidConfig config) : base(prefab, config) { }

    }
}