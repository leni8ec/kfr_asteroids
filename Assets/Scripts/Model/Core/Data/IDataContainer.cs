using Model.Core.Data.State.Base;
using Model.Core.Data.Unity.Config.Base;

namespace Model.Core.Data {
    public interface IDataContainer<out TState, out TConfig>
        where TState : IStateData, new()
        where TConfig : IConfigData {

        public TConfig Config { get; }
        public TState State { get; }
    }
}