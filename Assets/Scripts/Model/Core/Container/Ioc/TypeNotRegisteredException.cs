using System;

namespace Model.Core.Container.Ioc {
    public class TypeNotRegisteredException : Exception {
        public TypeNotRegisteredException(string message)
            : base(message) { }
    }
}