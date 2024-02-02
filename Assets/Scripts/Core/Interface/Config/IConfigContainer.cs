using UnityEngine;

namespace Core.Interface.Config {
    public interface IConfigContainer<out T> where T : ScriptableObject {
        public T Data { get; }
    }
}