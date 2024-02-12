using Model.Core.Interface.Config;
using Model.Core.Interface.State;

namespace Model.Core.Interface.Containers {
    public interface IDataContainer<out TState, out TConfig>
        where TState : IStateData, new()
        where TConfig : IConfigData {

        public TConfig Config { get; }
        public TState State { get; }
    }
}