using Core.Base;
using Core.Config;
using Core.Objects;
using Core.Pools.Base;
using Core.State;
using UnityEngine;

namespace Core.Pools {
    public class UfoPool : EntityPool<Ufo, UfoState, UfoConfig> {

        public UfoPool(GameObject prefab, UfoConfig config) : base(prefab, config) { }

    }
}