using Model.Core.Adapters;
using Model.Core.Data;
using Model.Core.Entity;
using Model.Core.Unity.Data.Config;
using Model.Domain.Systems.Base;

namespace Model.Domain.Systems {
    public class PlayerSystem : SystemBase {
        private Player Player { get; }

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
            Player.State.Active.Value = true;
        }

        private void Reset() {
            Player.State.Active.Value = false;
            Player.Reset();
        }


        private Player CreatePlayer(PlayerConfig playerConfig) {
            Player player = new Player();
            player.Create(playerConfig);
            return player;
        }

    }
}