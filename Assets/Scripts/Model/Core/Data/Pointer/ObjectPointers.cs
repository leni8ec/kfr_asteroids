using System.Collections.Generic;

namespace Model.Core.Data.Pointer {
    
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
}