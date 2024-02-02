using Core.Config;
using Core.Objects;
using Domain.Systems.Gameplay;
using UnityEngine;

namespace Domain.Systems.Audio {
    public class AudioSystem {
        private readonly SoundsConfig config;

        public AudioSystem(SoundsConfig config) {
            this.config = config;

            Subscribe();
        }


        private void Subscribe() {
            PlayerSystem.Fire1Event += () => Play(config.fire1);
            PlayerSystem.Fire2Event += () => Play(config.fire2);

            Asteroid.Explosion += asteroid => {
                if (asteroid.size == Asteroid.Size.Large) Play(config.explosionLarge);
                else if (asteroid.size == Asteroid.Size.Medium) Play(config.explosionMedium);
                else if (asteroid.size == Asteroid.Size.Small) Play(config.explosionSmall);
            };

            Ufo.Explosion += () => Play(config.explosionMedium);

        }

        private void Play(AudioClip clip) {
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        }

    }
}