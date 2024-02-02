using UnityEngine;

namespace Framework.Config {
    public interface IConfigContainer<out T> where T : ScriptableObject {
        public T Data { get; }
    }
}