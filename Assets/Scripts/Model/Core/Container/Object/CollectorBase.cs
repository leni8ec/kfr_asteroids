using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Core.Container.Object {
    public abstract class CollectorBase<TObject> : ICollector {
        private readonly IDictionary<Type, TObject> objects = new Dictionary<Type, TObject>();
        public ICollection<TObject> Values => objects.Values;

        public T Get<T>() where T : TObject {
            return (T)objects[typeof(T)];
        }

        public void Add<T>(T value) where T : TObject {
            objects.Add(typeof(T), value);
        }

        private readonly TypeObjectPointers<TObject, object> pointers = new();
        public TypeObjectPointers<TObject, object> Pointers => pointers;

        // public T Get<T, TPointer>(TPointer pointer) where T : TObject {
        //     return (T)pointers.Get<T>(pointer);
        // }

        public void Add<T, TPointer>(T value, TPointer pointer) where T : TObject {
            pointers.Add(pointer, value);
        }
    }


    public class TypeObjectPointers<TObject, TPointer> : IEnumerable<IObjectPointers> {

        private readonly IDictionary<Type, ObjectPointers<TObject, TPointer>> typesPointers = new Dictionary<Type, ObjectPointers<TObject, TPointer>>();
        public int Count => typesPointers.Count;

        public TObject Get<T>(TPointer pointer) where T : TObject {
            typesPointers.TryGetValue(typeof(T), out ObjectPointers<TObject, TPointer> objects);
            if (objects != null) return objects[pointer];

            Debug.LogError("");
            throw new NullReferenceException();
        }

        public void Add<T>(TPointer pointer, T value) where T : TObject {
            typesPointers.TryGetValue(typeof(T), out ObjectPointers<TObject, TPointer> objects);
            if (objects == null) typesPointers.Add(typeof(T), objects = new ObjectPointers<TObject, TPointer>());
            objects.Add(pointer, value);
        }


        public IEnumerator<IObjectPointers> GetEnumerator() {
            foreach (ObjectPointers<TObject, TPointer> objectPointers in typesPointers.Values) {
                yield return objectPointers;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }


    public class ObjectPointers<TObject, TPointer> : IObjectPointers {
        private readonly IDictionary<TPointer, TObject> pointers = new Dictionary<TPointer, TObject>();

        public TObject Get(TPointer pointer) {
            return pointers[pointer];
        }

        public void Add(TPointer pointer, TObject value) {
            pointers.Add(pointer, value);
        }

        public TObject this[TPointer pointer] => Get(pointer);
    }

    public interface IObjectPointers { }

}