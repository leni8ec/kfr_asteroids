using System;

namespace Model.Data.Reactive {

    public class ReactiveProperty<T> : ReactivePropertyBase<T>, IReactiveProperty<T> {

        public override event Action<T> Changed;

        private T value;
        public override T Value {
            get => value;
            set {
                if (Equals(this.value, value)) return;
                this.value = value;
                Changed?.Invoke(this.value);
            }
        }

        public ReactiveProperty() { }

        /// <summary>
        /// Init property with default value
        /// </summary>
        /// <param name="value">default value</param>
        public ReactiveProperty(T value) {
            this.value = value;
        }


        /// <summary>
        /// Set value without reactive callback
        /// </summary>
        public void Set(T value) {
            this.value = value;
        }

        /// <summary>
        /// Reset value without reactive callback
        /// </summary>
        public void Reset() {
            value = default;
        }

    }
}