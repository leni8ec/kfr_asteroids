using Core.Config;
using Core.Input;
using Core.Interface.Base;
using Core.Objects;
using Core.State;
using Core.Unity;
using Domain.Systems.Game;
using UnityEngine;

namespace Domain.Systems.Gameplay {
    public class PlayerSystem : IUpdate {
        private PlayerState State { get; }
        public Player Player { get; }

        private bool active;

        public PlayerSystem(PlayerState state, ConfigCollector configCollector, PrefabCollector prefabCollector) {
            State = state;


            // Player
            Player = CreatePlayer(prefabCollector.player, configCollector.player);

            // Input listeners
            InputController.Move += Player.Move;
            InputController.Rotate += Player.Rotate;

            // Game state listeners
            GameStateSystem.NewGameEvent += Play;
            GameStateSystem.GameOverEvent += Reset;
        }

        private void Play() {
            active = true;
            Player.gameObject.SetActive(true);
        }

        private void Reset() {
            active = false;
            Player.Reset();
            State.Reset();
            Player.gameObject.SetActive(false);
        }


        private Player CreatePlayer(GameObject playerPrefab, PlayerConfig playerConfig) {
            GameObject playerObject = Object.Instantiate(playerPrefab);
            Player targetPlayer = playerObject.GetComponent<Player>();
            targetPlayer.SetData(playerConfig);
            return targetPlayer;
        }

        public void Upd(float deltaTime) {
            //
        }
    }
}