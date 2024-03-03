using Control.Services.DI.Ioc;
using Model.Domain.Processors;
using Model.Domain.Systems.Base;
using Model.Domain.Systems.Interface;

namespace Control.Starter {
    public class SystemsRegistry {
        private readonly ISystemsProcessor processor;
        private readonly IDependencyContainer container;

        public SystemsRegistry(ISystemsProcessor processor, IDependencyContainer container) {
            this.processor = processor;
            this.container = container;
        }

        public void AddSystems() {
            // Resolve Systems              Order of initialization:
            Add<IAudioSystem>(); //            -1. Audio System (temp hack for disable sounds on GameOver)
            Add<IEntitiesSystem>(); //          0. Entities managers
            Add<IPlayerSystem>(); //            1. Player (player control)
            Add<IWeaponSystem>(); //            2. Weapon (spawn ammo)
            Add<IEnemiesSystem>(); //           3. Enemies (spawn enemies)
            Add<IInfinityScreenSystem>(); //    4. Infinity screen
            Add<IEntityUpdateSystem>(); //      5. Entities update
            Add<ICollisionSystem>(); //         6. Collision
            Add<IScoreSystem>();
            Add<IGameStateSystem>(); //         [Last] NewGame event
        }

        private void Add<T>() where T : ISystem {
            T system = container.Resolve<T>();
            processor.Add(system);
        }

    }
}