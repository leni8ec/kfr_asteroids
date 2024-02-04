using System.Collections.Generic;
using Core.Base;
using Core.Config;
using Core.Objects;
using Core.State;
using Core.Unity;
using Domain.Base;
using Domain.Systems.Game;
using UnityEngine;

namespace Domain.Systems.Gameplay {
    public class WeaponSystem : SystemBase, IUpdateSystem {
        private WeaponState State { get; }
        private BulletConfig Ammo1Config { get; }
        private LaserConfig Ammo2Config { get; }

        private EntityPool<Bullet, BulletConfig> Ammo1Pool { get; }
        private EntityPool<Laser, LaserConfig> Ammo2Pool { get; }

        private List<Bullet> ActiveBullets => Ammo1Pool.active;
        private List<Laser> ActiveLasers => Ammo2Pool.active;

        private float Fire1Delay => 1 / Ammo1Config.fireRate;
        private float Fire2Delay => 1 / Ammo2Config.fireRate;

        // Events
        public delegate void FireEvent();
        public static event FireEvent Fire1Event;
        public static event FireEvent Fire2Event;

        private Player Player { get; }

        private bool active;

        public WeaponSystem(StateCollector state, ConfigCollector configCollector, PrefabCollector prefabCollector) {
            State = state.weapon;
            Player = state.objects.player;

            // Fill objects state
            ObjectsState objects = state.objects;
            objects.ammo1Pool = new EntityPool<Bullet, BulletConfig>(prefabCollector.bullet, configCollector.bullet);
            objects.ammo2Pool = new EntityPool<Laser, LaserConfig>(prefabCollector.laser, configCollector.laser);

            // Link properties
            Ammo1Pool = objects.ammo1Pool;
            Ammo2Pool = objects.ammo2Pool;

            Ammo1Config = configCollector.bullet;
            Ammo2Config = configCollector.laser;

            // Game state listeners
            GameStateSystem.NewGameEvent += Play;
            GameStateSystem.GameOverEvent += Reset;
        }

        private void Play() {
            active = true;
            Player.gameObject.SetActive(true);
            State.laserShotCountdownDuration = Ammo2Config.shotRestoreCountdown;
        }

        private void Reset() {
            active = false;
            for (int i = ActiveBullets.Count - 1; i >= 0; i--) ActiveBullets[i].Reset();
            for (int i = ActiveLasers.Count - 1; i >= 0; i--) ActiveLasers[i].Reset();
            Player.Reset();
            State.Reset();
            Player.gameObject.SetActive(false);
        }

        public void Upd(float deltaTime) {
            if (!active) return;

            bool fired = State.FireState.Value != WeaponState.Weapon.Empty;
            if (fired && State.FireState.Value.HasFlag(WeaponState.Weapon.Gun) && State.fire1Countdown <= 0) {
                State.fire1Countdown = Fire1Delay;
                SpawnBullet();
                Fire1Event?.Invoke();
            }

            if (fired && State.FireState.Value.HasFlag(WeaponState.Weapon.Laser) && State.fire2Countdown <= 0 && State.laserShotsCount > 0) {
                State.fire2Countdown = Fire2Delay;
                State.laserShotsCount--;
                SpawnLaser();
                Fire2Event?.Invoke();
            }

            if (State.fire1Countdown > 0) State.fire1Countdown -= deltaTime;
            if (State.fire2Countdown > 0) State.fire2Countdown -= deltaTime;

            // Laser
            if (State.laserShotsCount < Ammo2Config.maxShotsCount) {
                if ((State.laserShotCountdownDuration -= deltaTime) <= 0) {
                    State.laserShotCountdownDuration = Ammo2Config.shotRestoreCountdown;
                    State.laserShotsCount++;
                }
            }
        }

        private void SpawnBullet() {
            Bullet bullet = Ammo1Pool.Take();
            Transform playerTransform = Player.transform;
            bullet.Set(Player.WeaponWorldPosition, playerTransform.up);
            bullet.Fire();
        }

        private void SpawnLaser() {
            Laser laser = Ammo2Pool.Take();
            Transform playerTransform = Player.transform;
            laser.Set(playerTransform.position, playerTransform.up);
            laser.Fire();
        }
    }
}