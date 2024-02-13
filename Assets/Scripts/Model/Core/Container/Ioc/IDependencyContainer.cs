using System;

namespace Model.Core.Container.Ioc {
    public interface IDependencyContainer {

        void Register<TConcrete>(LifeCycle lifeCycle);
        void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle);
        void Register<TConcrete>(TConcrete instance, LifeCycle lifeCycle); // For predefined instances
        void Register<TTypeToResolve, TConcrete>(TTypeToResolve instance, LifeCycle lifeCycle); // For predefined instances

        void RegisterByInstanceType<TConcrete>(TConcrete instance, LifeCycle lifeCycle );
        void RegisterByInstanceType2<TTypeToResolve>(TTypeToResolve instance, LifeCycle lifeCycle);

        TTypeToResolve Resolve<TTypeToResolve>();
        object Resolve(Type typeToResolve);

        TTypeToResolve ResolveUnregistered<TTypeToResolve>(); // For not required register dependencies(eq: Systems)

    }
}