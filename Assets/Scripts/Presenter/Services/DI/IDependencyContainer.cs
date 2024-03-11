using System;

namespace Presenter.Services.DI {
    public interface IDependencyContainer {

        void Register<TConcrete>(LifeCycle lifeCycle = default);
        void Register<TTypeToResolve, TConcrete>(LifeCycle lifeCycle = default);

        // Predefined instances
        void Register<TConcrete>(TConcrete instance);
        void Register<TTypeToResolve, TConcrete>(TConcrete instance);
        void Register<TConcrete>(Type typeToResolve, TConcrete instance);
        void Register<TConcrete>(Type typeToResolve, Type typeConcrete, TConcrete instance);

        // Resolvers
        TTypeToResolve Resolve<TTypeToResolve>();
        object Resolve(Type typeToResolve);

        TTypeToResolve ResolveUnregistered<TTypeToResolve>();

    }
}