using System;

namespace Model.Core.State.Base {
    public class ValueChange<T> {
        private T value;

        public T Value {
            get => value;
            set {
                if (Equals(this.value, value)) return;
                this.value = value;
                Changed?.Invoke(this.value);
            }
        }

        public event Action<T> Changed;

        public void Reset() {
            value = default;
        }
    }
}