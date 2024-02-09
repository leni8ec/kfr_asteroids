using Model.Core.Data.State;
using Model.Core.Objects;
using Model.Core.Pools.Base;
using Model.Core.Unity.Data.Config;

namespace Model.Core.Pools {
    public class AsteroidPool : EntityPool<Asteroid, AsteroidState, AsteroidConfig> {

        public AsteroidPool(AsteroidConfig config) : base(config) { }
    }
}