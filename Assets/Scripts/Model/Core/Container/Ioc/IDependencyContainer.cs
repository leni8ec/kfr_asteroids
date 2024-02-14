using System;

namespace Model.Core.Container.Ioc {
    public interface IDependencyContainer {

        void Register<TConcrete>(LifeCycle lifeCycle);
        void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle);

        // Predefined instances
        void Register<TConcrete>(TConcrete instance, LifeCycle lifeCycle);
        void Register<TTypeToResolve, TConcrete>(TConcrete instance, LifeCycle lifeCycle);
        void Register<TConcrete>(Type typeToResolve, TConcrete instance, LifeCycle lifeCycle);
        void Register<TConcrete>(Type typeToResolve, Type typeConcrete, TConcrete instance, LifeCycle lifeCycle);


        TTypeToResolve Resolve<TTypeToResolve>();
        object Resolve(Type typeToResolve);

        TTypeToResolve ResolveUnregistered<TTypeToResolve>();

    }
}