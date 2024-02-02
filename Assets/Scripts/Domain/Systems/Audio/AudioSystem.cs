using Core.Config;
using Core.Objects;
using Domain.Systems.Gameplay;
using UnityEngine;

namespace Domain.Systems.Audio {
    public class AudioSystem {
        private SoundsConfig Config { get; }

        public AudioSystem(SoundsConfig config) {
            Config = config;

            Subscribe();
        }


        private void Subscribe() {
            PlayerSystem.Fire1Event += () => Play(Config.fire1);
            PlayerSystem.Fire2Event += () => Play(Config.fire2);

            Asteroid.Explosion += asteroid => {
                if (asteroid.size == Asteroid.Size.Large) Play(Config.explosionLarge);
                else if (asteroid.size == Asteroid.Size.Medium) Play(Config.explosionMedium);
                else if (asteroid.size == Asteroid.Size.Small) Play(Config.explosionSmall);
            };

            Ufo.Explosion += () => Play(Config.explosionMedium);

        }

        private void Play(AudioClip clip) {
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        }

    }
}