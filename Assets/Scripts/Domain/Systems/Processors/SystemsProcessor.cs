using Core.Interface.Base;
using Core.State;
using Core.Unity;
using Domain.Systems.Audio;
using Domain.Systems.Collision;
using Domain.Systems.Game;
using Domain.Systems.Gameplay;

namespace Domain.Systems.Processors {
    public class SystemsProcessor : IUpdate {

        private readonly CollisionSystem collisionSystem;
        private readonly WorldSystem worldSystem;
        private readonly PlayerSystem playerSystem;
        private readonly WeaponSystem weaponSystem;
        private readonly ScoreSystem scoreSystem;
        private readonly AudioSystem audioSystem;
        private readonly GameStateSystem gameStateSystem;

        public SystemsProcessor(StateCollector state, ConfigCollector config, PrefabCollector prefab) {

            // Systems and processors
            playerSystem = new PlayerSystem(state, config, prefab);
            weaponSystem = new WeaponSystem(state, config, prefab);
            worldSystem = new WorldSystem(state, config, prefab);
            collisionSystem = new CollisionSystem(state, config, prefab);
            scoreSystem = new ScoreSystem(state, config, prefab);
            audioSystem = new AudioSystem(state, config, prefab);

            gameStateSystem = new GameStateSystem(state.game);
        }

        public void Upd(float deltaTime) {
            worldSystem.Upd(deltaTime);
            playerSystem.Upd(deltaTime);
            weaponSystem.Upd(deltaTime);
            collisionSystem.Upd(deltaTime);
        }
    }
}