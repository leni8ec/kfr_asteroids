using Model.Core.Config;
using Model.Core.Interface.View;
using Model.Core.Objects;
using Model.Core.State;
using Model.Core.Unity;
using Model.Domain.Systems.Base;
using UnityEngine;

namespace Model.Domain.Systems {
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
            Player.GameObject.SetActive(false);
            Player.Reset();
        }


        private Player CreatePlayer(GameObject playerPrefab, PlayerConfig playerConfig) {
            GameObject playerObject = Object.Instantiate(playerPrefab);
            Player targetPlayer = (Player)playerObject.GetComponent<IEntityView>().EntityLink;
            targetPlayer.SetConfig(playerConfig);
            return targetPlayer;
        }


        public void Upd(float deltaTime) {
            if (!active) return;

        }
    }
}