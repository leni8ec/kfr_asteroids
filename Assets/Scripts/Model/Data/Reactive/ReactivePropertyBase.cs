using System;

namespace Model.Data.Reactive {
#pragma warning disable CS0660, CS0661
    public abstract class ReactivePropertyBase<T> : IReactiveProperty<T> {

        public abstract T Value { get; set; }

        public abstract event Action<T> Changed;

        public static implicit operator T(ReactivePropertyBase<T> property) => property.Value;
        // public static explicit operator ReactivePropertyBase<T>(T value) => new ReactiveProperty<T>(value);


        public static bool operator ==(ReactivePropertyBase<T> lhs, ReactivePropertyBase<T> rhs) {
            return lhs is not null && rhs is not null && lhs.Value.Equals(rhs.Value);
        }

        public static bool operator !=(ReactivePropertyBase<T> lhs, ReactivePropertyBase<T> rhs) {
            return !(lhs == rhs);
        }

        public static bool operator ==(ReactivePropertyBase<T> lhs, T rhsVal) {
            return lhs?.Value.Equals(rhsVal) == true;
        }

        public static bool operator !=(ReactivePropertyBase<T> lhs, T rhs) {
            return !(lhs == rhs);
        }

        public static bool operator ==(T lhsVal, ReactivePropertyBase<T> rhs) {
            return rhs?.Equals(lhsVal) == true;
        }

        public static bool operator !=(T lhsVal, ReactivePropertyBase<T> rhs) {
            return !(lhsVal == rhs);
        }

    }
}