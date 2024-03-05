using Core.Processors;
using Core.Systems.Base;
using Core.Systems.Interface;
using Presenter.Services.DI.Ioc;

namespace Presenter.Starter {
    public class SystemsRegistry {
        private readonly IDependencyContainer container;

        public SystemsRegistry(IDependencyContainer container) {
            this.container = container;
        }

        public void ObtainSystems(ISystemsProcessor processor) {
            // Resolve Systems                      Order of initialization:
            Add<IAudioSystem>(processor); //            -1. Audio System (temp hack for disable sounds on GameOver)
            Add<IEntitiesSystem>(processor); //          0. Entities managers
            Add<IPlayerSystem>(processor); //            1. Player (player control)
            Add<IWeaponSystem>(processor); //            2. Weapon (spawn ammo)
            Add<IEnemiesSystem>(processor); //           3. Enemies (spawn enemies)
            Add<IInfinityScreenSystem>(processor); //    4. Infinity screen
            Add<IEntityUpdateSystem>(processor); //      5. Entities update
            Add<ICollisionSystem>(processor); //         6. Collision
            Add<IScoreSystem>(processor);
            Add<IGameStateSystem>(processor); //         [Last] NewGame event
        }

        private void Add<T>(ISystemsProcessor processor) where T : ISystem {
            T system = container.Resolve<T>();
            processor.Add(system);
        }

    }
}