using System;

namespace Presenter.Services.DI {
    public class TypeNotRegisteredException : Exception {
        public TypeNotRegisteredException(string message)
            : base(message) { }
    }
}