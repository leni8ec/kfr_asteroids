using Domain.Systems.Gameplay;
using Presentation.Data;
using Presentation.Objects;
using UnityEngine;

namespace Domain.Systems.Audio {
    public class AudioSystem {
        private readonly SoundsData data;

        public AudioSystem(SoundsData data) {
            this.data = data;

            Subscribe();
        }


        private void Subscribe() {
            PlayerSystem.Fire1Event += () => Play(data.fire1);
            PlayerSystem.Fire2Event += () => Play(data.fire2);

            Asteroid.Explosion += asteroid => {
                if (asteroid.size == Asteroid.Size.Large) Play(data.explosionLarge);
                else if (asteroid.size == Asteroid.Size.Medium) Play(data.explosionMedium);
                else if (asteroid.size == Asteroid.Size.Small) Play(data.explosionSmall);
            };

            Ufo.Explosion += () => Play(data.explosionMedium);

        }

        private void Play(AudioClip clip) {
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        }

    }
}