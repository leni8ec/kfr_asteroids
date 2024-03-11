using Core.Systems.Base;
using Core.Systems.Interface;
using JetBrains.Annotations;
using Model.Data.State;
using Model.Data.Unity.Config;
using Model.Entity;

namespace Core.Systems {
    [UsedImplicitly]
    public class PlayerSystem : SystemBase, IPlayerSystem {
        private Player Player { get; }

        public PlayerSystem(PlayerConfig config, ActiveEntitiesState entities) {
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