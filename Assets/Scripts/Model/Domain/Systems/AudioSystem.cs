using JetBrains.Annotations;
using Model.Core.Data.State;
using Model.Core.Data.State.Base;
using Model.Core.Entity;
using Model.Core.Game;
using Model.Core.Interface.Adapters;
using Model.Core.Unity.Data.Config;
using Model.Domain.Systems.Base;
using Model.Domain.Systems.Interface;
using UnityEngine;

namespace Model.Domain.Systems {
    [UsedImplicitly]
    public class AudioSystem : SystemBase, IAudioSystem, ICreateSystem {
        private SoundsConfig Config { get; }
        private IAudioAdapter Adapter { get; }

        private ActiveEntitiesState Entities { get; }
        private ValueChange<bool> PlayerActiveState { get; set; }

        public AudioSystem(SoundsConfig config, GameSystemState gameSystemState, IAudioAdapter audioAdapter, ActiveEntitiesState entities) {
            Config = config;
            Adapter = audioAdapter;
            Entities = entities;

            SubscribeEvents();
        }

        public void Create() {
            Entities.player.State.Active.Changed += active => {
                if (!active) PlaySound(Config.playerExplosion, true);
            };
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


        private void PlaySound(AudioClip clip, bool forced = false) {
            if (!forced && !Active) return;

            Adapter.PlaySound(clip);
        }

    }
}