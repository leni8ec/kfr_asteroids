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
        private WorldController worldController;
        private PlayerSystem playerSystem;
        private InputController inputController;
        private GameStateController gameStateController;

        private void Awake() {
            // Gui - Init first
            guiController = GuiController.Handler;
            Player player = guiController.prefabCollector.player.GetComponent<Player>();

            updateProcessor = new UpdateProcessor();
            collisionSystem = new CollisionSystem(player);
            inputController = new InputController();
            worldController = new WorldController(guiController.dataCollector, guiController.prefabCollector);
            playerSystem = new PlayerSystem(guiController.dataCollector, guiController.prefabCollector);
            gameStateController = new GameStateController();

        }

        // Start is called before the first frame update
        private void Start() { }

        // Update is called once per frame
        private void Update() {
            float deltaTime = Time.deltaTime;
            updateProcessor.Upd(deltaTime);
            collisionSystem.Upd(deltaTime);
        }
    }
}