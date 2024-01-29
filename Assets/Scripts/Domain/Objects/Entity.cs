using UnityEngine;

namespace Domain.Objects {
    public abstract class Entity<T> where T : ScriptableObject {

        protected T data;

        protected Entity(T data) {
            this.data = data;
        }

    }
}