using Model.Core.Data.State;
using Model.Core.Entity;
using Model.Core.Pools.Base;
using Model.Core.Unity.Data.Config;

namespace Model.Core.Pools {
    public class AsteroidPool : EntityPool<Asteroid, AsteroidState, AsteroidConfig> {

        public AsteroidPool(AsteroidConfig config, int capacity) : base(new EntityFactory<Asteroid, AsteroidState, AsteroidConfig>(config), capacity) { }

    }
}