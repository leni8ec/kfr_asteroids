using Model.Core.Adapters;
using Model.Core.Data;
using Model.Core.Data.State;
using Model.Core.Entity;
using Model.Core.Interface.Entity;
using Model.Core.Pools;
using Model.Core.Pools.Base;
using Model.Core.Unity.Data.Config;
using Model.Domain.Systems.Base;
using UnityEngine;

namespace Model.Domain.Systems {
    public class WeaponSystem : SystemBase, IUpdateSystem {
        private WeaponSystemState State { get; }
        private BulletConfig Ammo1Config { get; }
        private LaserConfig Ammo2Config { get; }

        private EntityPool<Bullet, BulletAmmoState, BulletConfig> Ammo1Pool { get; }
        private EntityPool<Laser, LaserAmmoState, LaserConfig> Ammo2Pool { get; }

        private float Fire1Delay => 1 / Ammo1Config.fireRate;
        private float Fire2Delay => 1 / Ammo2Config.fireRate;

        // Events
        public delegate void FireEvent();
        public static event FireEvent Fire1Event;
        public static event FireEvent Fire2Event;

        private Player Player { get; }

        public WeaponSystem(DataCollector data, AdaptersCollector adapters) {
            State = data.States.weapon;
            Player = data.States.entity.player;

            // Fill entities state
            EntitiesState entities = data.States.entity;
            entities.ammo1Pool = new BulletPool(data.Configs.bullet);
            entities.ammo2Pool = new LaserPool(data.Configs.laser);

            // Link properties
            Ammo1Pool = entities.ammo1Pool;
            Ammo2Pool = entities.ammo2Pool;

            Ammo1Config = data.Configs.bullet;
            Ammo2Config = data.Configs.laser;

            // Game state events
            GameStateSystem.NewGameEvent += Play;
            GameStateSystem.GameOverEvent += Reset;
        }

        private void Play() {
            Enable();
            State.laserShotCountdownDuration = Ammo2Config.shotRestoreCountdown;
        }

        private void Reset() {
            Disable();
            State.Reset();

            // Destroy Ammo
            Ammo1Pool.ForEachActive(DestroyEntity);
            Ammo2Pool.ForEachActive(DestroyEntity);

            return;
            void DestroyEntity(IEntity entity) => entity.Destroy();
        }

        public void Upd(float deltaTime) {
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