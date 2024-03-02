using Model.Core.Data.State.Base;

namespace Model.Core.Data.Unity.Config.Base {
    public interface IConfigContainer<out T> where T : IStateData {
        public T Data { get; }
    }
}