using Core.State;
using Core.Unity;
using Domain.Systems.Audio;
using Domain.Systems.Collision;
using Domain.Systems.Game;
using Domain.Systems.Gameplay;
using Domain.Systems.Processors;
using UnityEngine;

namespace Domain {
    public class Bootstrapper : MonoBehaviour {
        private SceneData sceneData;

        private UpdateProcessor updateProcessor;
        private CollisionSystem collisionSystem;
        private WorldSystem worldSystem;
        private PlayerSystem playerSystem;
        private WeaponSystem weaponSystem;
        private ScoreSystem scoreSystem;
        private AudioSystem audioSystem;
        private GameStateSystem gameStateSystem;

        private void Awake() {
            // Data - init first
            StateCollector state = new();
            sceneData = SceneData.Handler;
            sceneData.SetGameData(state);

            // Systems and processors
            playerSystem = new PlayerSystem(state.Player, sceneData.configCollector, sceneData.prefabCollector);
            weaponSystem = new WeaponSystem(state.Weapon, playerSystem.Player, sceneData.configCollector, sceneData.prefabCollector);
            worldSystem = new WorldSystem(state.World, playerSystem.Player, weaponSystem.ActiveBullets, sceneData.configCollector, sceneData.prefabCollector, sceneData.mainCamera);
            collisionSystem = new CollisionSystem(playerSystem.Player, worldSystem.AsteroidPools, worldSystem.ActiveUfos, weaponSystem.ActiveBullets, weaponSystem.ActiveLasers);
            scoreSystem = new ScoreSystem(state.Score);
            audioSystem = new AudioSystem(sceneData.configCollector.sounds);

            gameStateSystem = new GameStateSystem(state.Game);
            updateProcessor = new UpdateProcessor();

            sceneData.SetPlayer(playerSystem.Player);
        }


        private void Update() {
            float deltaTime = Time.deltaTime;

            worldSystem.Upd(deltaTime);
            playerSystem.Upd(deltaTime);
            weaponSystem.Upd(deltaTime);
            updateProcessor.Upd(deltaTime);
            collisionSystem.Upd(deltaTime);
        }
    }
}