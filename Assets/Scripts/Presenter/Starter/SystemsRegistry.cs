using Core.Framework.Systems;
using Core.Framework.Systems.Cluster;
using Core.Systems.Interface;
using Presenter.Services.DI;

namespace Presenter.Starter {
    public class SystemsRegistry {
        private readonly IDependencyContainer container;

        public SystemsRegistry(IDependencyContainer container) {
            this.container = container;
        }

        public void ObtainSystems(ISystemsCluster cluster) {
            // Resolve Systems                      Order of initialization:
            Add<IAudioSystem>(cluster); //            -1. Audio System (temp hack for disable sounds on GameOver)
            Add<IEntitiesSystem>(cluster); //          0. Entities managers
            Add<IPlayerSystem>(cluster); //            1. Player (player control)
            Add<IWeaponSystem>(cluster); //            2. Weapon (spawn ammo)
            Add<IEnemiesSystem>(cluster); //           3. Enemies (spawn enemies)
            Add<IInfinityScreenSystem>(cluster); //    4. Infinity screen
            Add<IEntityUpdateSystem>(cluster); //      5. Entities update
            Add<ICollisionSystem>(cluster); //         6. Collision
            Add<IScoreSystem>(cluster);
            Add<IGameStateSystem>(cluster); //         [Last] NewGame event
        }

        private void Add<T>(ISystemsCluster cluster) where T : ISystem {
            T system = container.Resolve<T>();
            cluster.Add(system);
        }

    }
}