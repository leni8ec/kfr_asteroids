using Core.Config;
using Core.Objects;
using Core.State;
using Core.Unity;
using Domain.Systems.Gameplay;
using UnityEngine;

namespace Domain.Systems.Audio {
    public class AudioSystem {
        private SoundsConfig Config { get; }

        public AudioSystem(StateCollector state, ConfigCollector config, PrefabCollector prefab) {
            Config = config.sounds;

            Subscribe();
        }


        private void Subscribe() {
            WeaponSystem.Fire1Event += () => Play(Config.fire1);
            WeaponSystem.Fire2Event += () => Play(Config.fire2);

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