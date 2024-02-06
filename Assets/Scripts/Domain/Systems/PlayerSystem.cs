using Core.Config;
using Core.Interface.Objects;
using Core.Interface.View;
using Core.Objects;
using Core.State;
using Core.Unity;
using Domain.Base;
using UnityEngine;

namespace Domain.Systems {
    public class PlayerSystem : SystemBase, IUpdateSystem {
        private Player Player { get; }

        private bool active;

        public PlayerSystem(StateCollector state, ConfigCollector configCollector, PrefabCollector prefabCollector) {
            // Fill objects state
            Player createdPlayer = CreatePlayer(prefabCollector.player, configCollector.player);
            state.objects.player = createdPlayer;

            // Link properties
            Player = createdPlayer;

            // Game state listeners
            GameStateSystem.NewGameEvent += Play;
            GameStateSystem.GameOverEvent += Reset;
        }

        private void Play() {
            active = true;
            Player.GameObject.SetActive(true);
        }

        private void Reset() {
            active = false;
            Player.Reset();
            Player.GameObject.SetActive(false);
        }


        private Player CreatePlayer(GameObject playerPrefab, PlayerConfig playerConfig) {
            GameObject playerObject = Object.Instantiate(playerPrefab);
            Player targetPlayer = (Player)playerObject.GetComponent<IEntityView>().Entity;
            targetPlayer.SetConfig(playerConfig);
            return targetPlayer;
        }


        public void Upd(float deltaTime) {
            if (!active) return;

            // Update player
            Player.Upd(deltaTime);
        }
    }
}