using UnityEngine;

namespace Core.Unity.Helpers {
    /// <summary>
    /// It's roughly similar to Singleton, except that it does not create a new object
    /// </summary>
    public abstract class MonoBehaviourHandler<T> : MonoBehaviour where T : MonoBehaviour {

        private static T s_handler;
        public static T Handler {
            get {
                if (s_handler) return s_handler;
                if (Application.isPlaying) Debug.LogWarning($"'{typeof(T)}' Handler must be assigned on 'Awake' to avoid searching process (It can be performance critical).");
                s_handler = UnityHelper.FindComponent<T>();
                if (!s_handler) Debug.LogError($"'{typeof(T)}' isn't found for Handler!");
                return s_handler;
            }
            set => s_handler = value;
        }

        // Init handler on Awake
        protected virtual void Awake() {
            if (s_handler && s_handler != this) {
                Debug.LogWarning($"Handler: is already exists for: '{s_handler.gameObject.name}'" +
                                 $"\n Handler is updated to: '{this.gameObject.name}'");
            }

            s_handler = (T)(MonoBehaviour)this;
        }

        // Remove handler on Destroy 
        protected virtual void OnDestroy() {
            if (s_handler && s_handler == this) s_handler = null;
        }

    }
}