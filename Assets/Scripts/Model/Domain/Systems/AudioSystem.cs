using JetBrains.Annotations;
using Model.Core.Data.State;
using Model.Core.Data.State.Base;
using Model.Core.Entity;
using Model.Core.Game;
using Model.Core.Interface.Adapters;
using Model.Core.Unity.Data.Config;
using Model.Domain.Systems.Base;
using UnityEngine;

namespace Model.Domain.Systems {
    [UsedImplicitly]
    public class AudioSystem : SystemBase {
        private SoundsConfig Config { get; }

        private ValueChange<GameStatus> GameStatus { get; }
        private IAudioAdapter Adapter { get; }
        private ValueChange<bool> PlayerActiveState { get; }

        public AudioSystem(SoundsConfig config, GameSystemState gameSystemState, EntitiesState entities, IAudioAdapter audioAdapter) {
            Config = config;
            GameStatus = gameSystemState.Status;
            PlayerActiveState = entities.player.State.Active;

            Adapter = audioAdapter;

            SubscribeEvents();
        }

        private void SubscribeEvents() {
            // Game state events
            GameStateSystem.NewGameEvent += Enable;
            GameStateSystem.GameOverEvent += Disable;

            // Weapon events
            WeaponSystem.Fire1Event += () => PlaySound(Config.fire1);
            WeaponSystem.Fire2Event += () => PlaySound(Config.fire2);

            // Entities events
            Asteroid.ExplosionEvent += asteroid => {
                if (asteroid.Size == AsteroidConfig.Size.Large) PlaySound(Config.explosionLarge);
                else if (asteroid.Size == AsteroidConfig.Size.Medium) PlaySound(Config.explosionMedium);
                else if (asteroid.Size == AsteroidConfig.Size.Small) PlaySound(Config.explosionSmall);
            };

            Ufo.ExplosionEvent += () => PlaySound(Config.explosionMedium);

            PlayerActiveState.Changed += active => {
                if (!active) PlaySound(Config.playerExplosion, true);
            };
        }

        private void PlaySound(AudioClip clip, bool forced = false) {
            if (!forced && (!Active || GameStatus.Value != Core.Game.GameStatus.Playing)) return;
            Adapter.PlaySound(clip);
        }

    }
}