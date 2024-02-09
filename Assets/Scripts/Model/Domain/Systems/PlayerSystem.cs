using Model.Core.Adapters;
using Model.Core.Data;
using Model.Core.Entity;
using Model.Core.Unity.Data.Config;
using Model.Domain.Systems.Base;

namespace Model.Domain.Systems {
    public class PlayerSystem : SystemBase, IUpdateSystem {
        private Player Player { get; }

        private bool active;

        public PlayerSystem(DataCollector data, AdaptersCollector adapters) {
            // Fill entities state
            Player createdPlayer = CreatePlayer(data.Configs.player);
            data.States.entity.player = createdPlayer;

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


        private Player CreatePlayer(PlayerConfig playerConfig) {
            Player player = new Player();
            player.Create(playerConfig);
            return player;
        }


        public void Upd(float deltaTime) {
            if (!active) return;

        }
    }
}