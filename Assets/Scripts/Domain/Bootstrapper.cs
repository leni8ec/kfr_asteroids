using Domain.Systems.Collision;
using Domain.Systems.Gameplay;
using Domain.Systems.GameState;
using Domain.Systems.Input;
using Domain.Systems.Processors;
using Presentation.GUI;
using Presentation.Objects;
using UnityEngine;

namespace Domain {
    public class Bootstrapper : MonoBehaviour {
        private GuiController guiController;

        private UpdateProcessor updateProcessor;
        private CollisionSystem collisionSystem;
        private WorldSystem worldSystem;
        private PlayerSystem playerSystem;
        private InputController inputController;
        private GameStateController gameStateController;

        private void Awake() {
            // Gui - Init first
            guiController = GuiController.Handler;

            updateProcessor = new UpdateProcessor();
            worldSystem = new WorldSystem(guiController.dataCollector, guiController.prefabCollector);
            playerSystem = new PlayerSystem(guiController.dataCollector, guiController.prefabCollector);
            collisionSystem = new CollisionSystem(playerSystem.Player, worldSystem.ActiveAsteroids, worldSystem.ActiveUfos, playerSystem.ActiveBullets);
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