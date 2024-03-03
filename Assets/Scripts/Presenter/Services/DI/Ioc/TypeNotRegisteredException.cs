using System;

namespace Presenter.Services.DI.Ioc {
    public class TypeNotRegisteredException : Exception {
        public TypeNotRegisteredException(string message)
            : base(message) { }
    }
}