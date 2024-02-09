using Model.Core.Adapters;
using Model.Core.Data;
using Model.Core.Data.State.Base;
using Model.Core.Entity;
using Model.Core.Game;
using Model.Core.Unity.Data.Config;
using Model.Domain.Systems.Base;
using UnityEngine;

namespace Model.Domain.Systems {
    public class AudioSystem : SystemBase {
        private SoundsConfig Config { get; }

        private ValueChange<GameStatus> GameStatus { get; }
        private bool active;

        public AudioSystem(DataCollector data, AdaptersCollector adapters) {
            Config = data.Configs.sounds;
            GameStatus = data.States.game.Status;

            Subscribe();

            // Game state
            GameStateSystem.NewGameEvent += () => active = true;
            GameStateSystem.GameOverEvent += () => active = false;
        }

        private void Subscribe() {
            WeaponSystem.Fire1Event += () => Play(Config.fire1);
            WeaponSystem.Fire2Event += () => Play(Config.fire2);

            Asteroid.ExplosionEvent += asteroid => {
                if (asteroid.Size == AsteroidConfig.Size.Large) Play(Config.explosionLarge);
                else if (asteroid.Size == AsteroidConfig.Size.Medium) Play(Config.explosionMedium);
                else if (asteroid.Size == AsteroidConfig.Size.Small) Play(Config.explosionSmall);
            };

            Ufo.ExplosionEvent += () => Play(Config.explosionMedium);

        }

        private void Play(AudioClip clip) {
            if (!active || GameStatus.Value != Core.Game.GameStatus.Playing) return;
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        }

    }
}