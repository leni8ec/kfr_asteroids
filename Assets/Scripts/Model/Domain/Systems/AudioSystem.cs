using Model.Core.Adapters;
using Model.Core.Data;
using Model.Core.Data.State.Base;
using Model.Core.Entity;
using Model.Core.Game;
using Model.Core.Interface.Adapters;
using Model.Core.Unity.Data.Config;
using Model.Domain.Systems.Base;
using UnityEngine;

namespace Model.Domain.Systems {
    public class AudioSystem : SystemBase {
        private SoundsConfig Config { get; }

        private ValueChange<GameStatus> GameStatus { get; }
        private IAudioAdapter Adapter { get; }

        public AudioSystem(DataCollector data, AdaptersCollector adapters) {
            Config = data.Configs.sounds;
            GameStatus = data.States.game.Status;

            Adapter = adapters.audio;

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
        }

        private void PlaySound(AudioClip clip) {
            if (!Active || GameStatus.Value != Core.Game.GameStatus.Playing) return;
            Adapter.PlaySound(clip);
        }

    }
}