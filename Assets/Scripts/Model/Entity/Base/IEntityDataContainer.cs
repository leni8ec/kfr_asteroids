using Model.Data.State.Base;
using Model.Data.Unity.Config.Base;

namespace Model.Entity.Base {
    public interface IEntityDataContainer<out TState, out TConfig>
        where TState : IStateData, new()
        where TConfig : IConfigData {

        public TConfig Config { get; }
        public TState State { get; }
    }
}