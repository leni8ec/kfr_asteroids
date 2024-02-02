using System;

namespace Core.Base {
    public class ChangedValue<T> {
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