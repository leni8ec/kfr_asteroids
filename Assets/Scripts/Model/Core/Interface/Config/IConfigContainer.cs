using Model.Core.Interface.State;

namespace Model.Core.Interface.Config {
    public interface IConfigContainer<out T> where T : IStateData {
        public T Data { get; }
    }
}