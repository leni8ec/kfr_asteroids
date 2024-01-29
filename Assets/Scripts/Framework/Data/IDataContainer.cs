using UnityEngine;

namespace Framework.Data {
    public interface IDataContainer<out T> where T : ScriptableObject {
        public T Data { get; }
    }
}