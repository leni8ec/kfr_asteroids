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
        private AudioSystem audioSystem;
        private GameStateController gameStateController;

        private void Awake() {
            // Gui - Init first
            sceneData = SceneData.Handler;

            updateProcessor = new UpdateProcessor();
            playerSystem = new PlayerSystem(sceneData.configCollector, sceneData.prefabCollector);
            worldSystem = new WorldSystem(playerSystem.Player, playerSystem.ActiveBullets, sceneData.configCollector, sceneData.prefabCollector, sceneData.mainCamera);
            collisionSystem = new CollisionSystem(playerSystem.Player, worldSystem.AsteroidPools, worldSystem.ActiveUfos, playerSystem.ActiveBullets, playerSystem.ActiveLasers);
            audioSystem = new AudioSystem(sceneData.configCollector.sounds);
            gameStateController = new GameStateController();

        }

        // Start is called before the first frame update
        private void Start() { }

        // Update is called once per frame
        private void Update() {
            float deltaTime = Time.deltaTime;
            worldSystem.Upd(deltaTime);
            playerSystem.Upd(deltaTime);
            updateProcessor.Upd(deltaTime);
            collisionSystem.Upd(deltaTime);
        }
    }
}