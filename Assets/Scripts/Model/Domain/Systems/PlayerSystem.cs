using JetBrains.Annotations;
using Model.Core.Data.State;
using Model.Core.Entity;
using Model.Core.Unity.Data.Config;
using Model.Domain.Systems.Base;
using Model.Domain.Systems.Interface;

namespace Model.Domain.Systems {
    [UsedImplicitly]
    public class PlayerSystem : SystemBase, IPlayerSystem {
        private Player Player { get; }

        public PlayerSystem(PlayerConfig config, EntitiesState entities) {
            // Fill entities state
            Player createdPlayer = CreatePlayer(config);
            entities.player = createdPlayer;

            // Link properties
            Player = createdPlayer;

            // Game state events
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