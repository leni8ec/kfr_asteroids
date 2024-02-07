using Model.Core.State.Base;
using UnityEngine;

namespace Model.Core.Interface.Containers {
    public interface IDataContainer<out TState, out TConfig>
        where TState : EntityState, new()
        where TConfig : ScriptableObject {

        public TConfig Config { get; }
        public TState State { get; }
    }
}