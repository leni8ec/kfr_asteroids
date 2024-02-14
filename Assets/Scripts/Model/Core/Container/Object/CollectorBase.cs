using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Model.Core.Container.Object {

    public abstract class CollectorBase<TObject> : ICollector {
        private readonly IDictionary<Type, TObject> objects = new Dictionary<Type, TObject>();
        public IDictionary<Type, TObject> Objects => objects;

        public T Get<T>() where T : TObject {
            return (T) objects[typeof(T)];
        }

        public void Add<T>(T value) where T : TObject {
            objects.Add(typeof(T), value);
        }

        private readonly TypeObjectPointers<object, TObject> pointers = new();
        public TypeObjectPointers<object, TObject> Pointers => pointers;
        public bool IsPointersExists => pointers.Count > 0;

        // => Get - not used
        // public T Get<T, TPointer>(TPointer pointer) where T : TObject {
        //     return (T)pointers.Get<T>(pointer);
        // }

        public void Add<T, TPointer>(T value, TPointer pointer) where T : TObject {
            pointers.Add(value, pointer);
        }

    }


    // todo: find a better naming for class
    /// <typeparam name="TPointer">Always is 'object'</typeparam>
    /// <typeparam name="TObject"></typeparam>
    public class TypeObjectPointers<TPointer, TObject> : IEnumerable<DictionaryEntry> {

        // => there's no way to do it with generic 'Dictionary<,>' due to the need for implicit conversions IDictionary
        // This cast is impossible: ObjectPointers<IConfigState, object> -> ObjectPointers<AsteroidConfigState, AsteroidConfig.Size>
        private readonly IDictionary typesPointers = new HybridDictionary();
        public int Count => typesPointers.Count;

        public void Add<TO, TP>(TO value, TP pointer) where TO : TObject where TP : TPointer {
            Type objectPointersType = typeof(ObjectPointers<TO, TP>);
            ObjectPointers<TO, TP> objectsPointers;

            if (!typesPointers.Contains(objectPointersType)) typesPointers.Add(objectPointersType, objectsPointers = new ObjectPointers<TO, TP>());
            else objectsPointers = (ObjectPointers<TO, TP>) typesPointers[objectPointersType];
            objectsPointers.Add(pointer, value);
        }


        public IEnumerator<DictionaryEntry> GetEnumerator() {
            foreach (DictionaryEntry objectPointers in typesPointers) {
                yield return objectPointers;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

    }

    // todo: find a better naming for class
    public class ObjectPointers<TObject, TPointer> : IObjectPointers {
        private readonly IDictionary<TPointer, TObject> pointers = new Dictionary<TPointer, TObject>();

        public TObject Get(TPointer pointer) {
            return pointers[pointer];
        }

        public void Add(TPointer pointer, object value) {
            pointers.Add(pointer, (TObject) value);
        }

        public TObject this[TPointer pointer] => Get(pointer);
    }

    public interface IObjectPointers { }

}