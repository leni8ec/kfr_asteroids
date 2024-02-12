using Model.Core.Data.State;
using Model.Core.Entity;
using Model.Core.Pools.Base;
using Model.Core.Unity.Data.Config;

namespace Model.Core.Pools {
    public class UfoPool : EntityPool<Ufo, UfoState, UfoConfig> {

        public UfoPool(UfoConfig config) : base(new EntityFactory<Ufo, UfoState, UfoConfig>(config)) { }

    }
}