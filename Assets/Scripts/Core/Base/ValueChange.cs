using System;

namespace Core.Base {
    public class ValueChange<T> {
        private T value;
        
        public T Value {
            get => value;
            set {
                this.value = value;
                Changed?.Invoke(this.value);
            }
        }

        public event Action<T> Changed;
    }
}