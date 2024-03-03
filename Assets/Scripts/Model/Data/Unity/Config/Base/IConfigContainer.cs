using Model.Data.State.Base;

namespace Model.Data.Unity.Config.Base {
    public interface IConfigContainer<out T> where T : IStateData {
        public T Data { get; }
    }
}