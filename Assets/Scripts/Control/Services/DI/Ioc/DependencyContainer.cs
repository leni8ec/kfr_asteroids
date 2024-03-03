using System;
using System.Collections.Generic;
using System.Linq;

namespace Control.Services.DI.Ioc {
    public class DependencyContainer : IDependencyContainer {

        private readonly IList<RegisteredObject> registeredObjects = new List<RegisteredObject>();

        private const LifeCycle LifeCycleDefault = LifeCycle.Singleton;


        public void Register<TConcrete>(LifeCycle lifeCycle = LifeCycleDefault) {
            Register<TConcrete, TConcrete>(lifeCycle);
        }

        public void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle = LifeCycleDefault) {
            registeredObjects.Add(new RegisteredObject(typeof(TTypeToResolve), typeof(TConcrete), lifeCycle));
        }


        public void Register<TConcrete>(TConcrete instance, LifeCycle lifeCycle = LifeCycleDefault) {
            Register<TConcrete, TConcrete>(instance, lifeCycle);
        }

        public void Register<TTypeToResolve, TConcrete>(TConcrete instance, LifeCycle lifeCycle = LifeCycleDefault) {
            Register(typeof(TTypeToResolve), typeof(TConcrete), instance, lifeCycle);
        }

        public void Register<TConcrete>(Type typeToResolve, TConcrete instance, LifeCycle lifeCycle = LifeCycleDefault) {
            registeredObjects.Add(new RegisteredObject(typeToResolve, typeof(TConcrete), lifeCycle, instance));
        }

        public void Register<TConcrete>(Type typeToResolve, Type typeConcrete, TConcrete instance, LifeCycle lifeCycle = LifeCycleDefault) {
            registeredObjects.Add(new RegisteredObject(typeToResolve, typeConcrete, lifeCycle, instance));
        }


        public TTypeToResolve Resolve<TTypeToResolve>() {
            return (TTypeToResolve) ResolveObject(typeof(TTypeToResolve));
        }

        public object Resolve(Type typeToResolve) {
            return ResolveObject(typeToResolve);
        }

        public TTypeToResolve ResolveUnregistered<TTypeToResolve>() {
            RegisteredObject unregisteredObject = new RegisteredObject(typeof(TTypeToResolve), typeof(TTypeToResolve), LifeCycle.Transient);
            return (TTypeToResolve) GetInstance(unregisteredObject);
        }


        // public void Set<TType>(TType resolvedObject) { }

        private object ResolveObject(Type typeToResolve) {
            // RegisteredObject registeredObject = registeredObjects.FirstOrDefault(o => typeToResolve.IsAssignableFrom(o.TypeToResolve)); // hack (not used)
            RegisteredObject registeredObject = registeredObjects.FirstOrDefault(o => o.TypeToResolve == typeToResolve);
            if (registeredObject == null) {
                throw new TypeNotRegisteredException($"The type {typeToResolve.Name} has not been registered");
            }
            return GetInstance(registeredObject);
        }

        private object GetInstance(RegisteredObject registeredObject) {
            if (registeredObject.Instance == null || registeredObject.LifeCycle == LifeCycle.Transient) {
                var parameters = ResolveConstructorParameters(registeredObject);
                registeredObject.CreateInstance(parameters.ToArray());
            }
            return registeredObject.Instance;
        }

        private IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject) {
            var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
            foreach (var parameter in constructorInfo.GetParameters()) {
                yield return ResolveObject(parameter.ParameterType);
            }
        }
    }
}