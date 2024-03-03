using System;

namespace Control.Services.DI.Ioc {
    public class RegisteredObject {

        public Type TypeToResolve { get; }
        public Type ConcreteType { get; }
        public LifeCycle LifeCycle { get; }

        public object Instance { get; private set; }

        public RegisteredObject(Type typeToResolve, Type concreteType, LifeCycle lifeCycle, object instance = null) {
            TypeToResolve = typeToResolve;
            ConcreteType = concreteType;
            LifeCycle = lifeCycle;

            if (instance != null) Instance = instance;
        }

        public void CreateInstance(params object[] args) {
            Instance = Activator.CreateInstance(ConcreteType, args);
        }
    }
}