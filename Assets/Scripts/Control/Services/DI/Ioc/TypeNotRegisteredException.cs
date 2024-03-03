using System;

namespace Control.Services.DI.Ioc {
    public class TypeNotRegisteredException : Exception {
        public TypeNotRegisteredException(string message)
            : base(message) { }
    }
}