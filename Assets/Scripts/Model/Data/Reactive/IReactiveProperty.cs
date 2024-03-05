using System;

namespace Model.Data.Reactive {
    /// <summary>
    /// ReadOnly 'ReactiveProperty' implementation 
    /// </summary>
    /// <typeparam name="T">Type of the property</typeparam>
    public interface IReactiveProperty<out T> {

        T Value { get; }

        event Action<T> Changed;

    }
}