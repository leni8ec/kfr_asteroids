using Core.Config;
using Core.Objects;
using Core.State;
using Core.Unity;
using Domain.Base;
using UnityEngine;

namespace Domain.Systems {
    public class AudioSystem : SystemBase {
        private SoundsConfig Config { get; }

        private bool active;

        public AudioSystem(StateCollector state, ConfigCollector config, PrefabCollector prefab) {
            Config = config.sounds;

            Subscribe();

            // Game state
            GameStateSystem.NewGameEvent += () => active = true;
            GameStateSystem.GameOverEvent += () => active = false;
        }

        private void Subscribe() {
            WeaponSystem.Fire1Event += () => Play(Config.fire1);
            WeaponSystem.Fire2Event += () => Play(Config.fire2);

            Asteroid.Explosion += asteroid => {
                if (asteroid.Size == AsteroidConfig.Size.Large) Play(Config.explosionLarge);
                else if (asteroid.Size == AsteroidConfig.Size.Medium) Play(Config.explosionMedium);
                else if (asteroid.Size == AsteroidConfig.Size.Small) Play(Config.explosionSmall);
            };

            Ufo.ExplosionEvent += () => Play(Config.explosionMedium);

        }

        private void Play(AudioClip clip) {
            if (!active) return;
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        }

    }
}