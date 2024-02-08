using Model.Core.Data.State;
using Model.Core.Objects;
using Model.Core.Pools.Base;
using Model.Core.Unity.Data.Config;
using UnityEngine;

namespace Model.Core.Pools {
    public class UfoPool : EntityPool<Ufo, UfoState, UfoConfig> {

        public UfoPool(GameObject prefab, UfoConfig config) : base(prefab, config) { }
    }
}