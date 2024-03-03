using System;

namespace Presenter.Services.DI.Ioc {
    public interface IDependencyContainer {

        void Register<TConcrete>(LifeCycle lifeCycle = default);
        void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle = default);

        // Predefined instances
        void Register<TConcrete>(TConcrete instance, LifeCycle lifeCycle = default);
        void Register<TTypeToResolve, TConcrete>(TConcrete instance, LifeCycle lifeCycle = default);
        void Register<TConcrete>(Type typeToResolve, TConcrete instance, LifeCycle lifeCycle = default);
        void Register<TConcrete>(Type typeToResolve, Type typeConcrete, TConcrete instance, LifeCycle lifeCycle = default);


        TTypeToResolve Resolve<TTypeToResolve>();
        object Resolve(Type typeToResolve);

        TTypeToResolve ResolveUnregistered<TTypeToResolve>();

    }
}