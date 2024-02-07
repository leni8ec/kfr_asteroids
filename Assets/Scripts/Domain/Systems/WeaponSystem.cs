using System.Collections.Generic;
using Core.Config;
using Core.Objects;
using Core.Pools;
using Core.Pools.Base;
using Core.State;
using Core.Unity;
using Domain.Base;
using UnityEngine;

namespace Domain.Systems {
    public class WeaponSystem : SystemBase, IUpdateSystem {
        private WeaponSystemState State { get; }
        private BulletConfig Ammo1Config { get; }
        private LaserConfig Ammo2Config { get; }

        private EntityPool<Bullet, BulletAmmoState, BulletConfig> Ammo1Pool { get; }
        private EntityPool<Laser, LaserAmmoState, LaserConfig> Ammo2Pool { get; }

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
            objects.ammo1Pool = new BulletPool(prefabCollector.bullet, configCollector.bullet);
            objects.ammo2Pool = new LaserPool(prefabCollector.laser, configCollector.laser);

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
            State.laserShotCountdownDuration = Ammo2Config.shotRestoreCountdown;
        }

        private void Reset() {
            active = false;
            for (int i = ActiveBullets.Count - 1; i >= 0; i--) ActiveBullets[i].Destroy();
            for (int i = ActiveLasers.Count - 1; i >= 0; i--) ActiveLasers[i].Destroy();
            State.Reset();
        }

        public void Upd(float deltaTime) {
            if (!active) return;

            bool fired = State.FireState.Value != WeaponSystemState.Weapon.Empty;
            if (fired && State.FireState.Value.HasFlag(WeaponSystemState.Weapon.Gun) && State.fire1Countdown <= 0) {
                State.fire1Countdown = Fire1Delay;
                SpawnBullet();
                Fire1Event?.Invoke();
            }

            if (fired && State.FireState.Value.HasFlag(WeaponSystemState.Weapon.Laser) && State.fire2Countdown <= 0 && State.laserShotsCount > 0) {
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
            Transform playerTransform = Player.Transform;
            bullet.Set(Player.WeaponWorldPosition, playerTransform.up);
            bullet.Fire();
        }

        private void SpawnLaser() {
            Laser laser = Ammo2Pool.Take();
            Transform playerTransform = Player.Transform;
            laser.Set(playerTransform.position, playerTransform.up);
            laser.Fire();
        }
    }
}