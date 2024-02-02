using Core.Data;
using Core.Unity;
using Domain.Systems.Audio;
using Domain.Systems.Collision;
using Domain.Systems.Gameplay;
using Domain.Systems.GameState;
using Domain.Systems.Processors;
using UnityEngine;

namespace Domain {
    public class Bootstrapper : MonoBehaviour {
        private SceneData sceneData;

        private UpdateProcessor updateProcessor;
        private CollisionSystem collisionSystem;
        private WorldSystem worldSystem;
        private PlayerSystem playerSystem;
        private ScoreSystem scoreSystem;
        private AudioSystem audioSystem;
        private GameStateSystem gameStateSystem;

        private void Awake() {
            // Data - init first
            DataCollector data = new();
            sceneData = SceneData.Handler;
            sceneData.SetGameData(data);

            // Systems and processors
            playerSystem = new PlayerSystem(data.Player, sceneData.configCollector, sceneData.prefabCollector);
            worldSystem = new WorldSystem(data.World, playerSystem.Player, playerSystem.ActiveBullets, sceneData.configCollector, sceneData.prefabCollector, sceneData.mainCamera);
            collisionSystem = new CollisionSystem(playerSystem.Player, worldSystem.AsteroidPools, worldSystem.ActiveUfos, playerSystem.ActiveBullets, playerSystem.ActiveLasers);
            scoreSystem = new ScoreSystem(data.Score);
            audioSystem = new AudioSystem(sceneData.configCollector.sounds);

            gameStateSystem = new GameStateSystem(data.GameState);
            updateProcessor = new UpdateProcessor();

            sceneData.SetPlayer(playerSystem.Player);
        }


        private void Update() {
            float deltaTime = Time.deltaTime;

            worldSystem.Upd(deltaTime);
            playerSystem.Upd(deltaTime);
            updateProcessor.Upd(deltaTime);
            collisionSystem.Upd(deltaTime);
        }
    }
}