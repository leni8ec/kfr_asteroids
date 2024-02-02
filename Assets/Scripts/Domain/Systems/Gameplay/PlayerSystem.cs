using System.Collections.Generic;
using Core.Base;
using Core.Config;
using Core.Input;
using Core.Interface.Base;
using Core.Interface.Objects;
using Core.Objects;
using Core.Unity;
using Domain.Systems.Collision;
using UnityEngine;

namespace Domain.Systems.Gameplay {
    public class PlayerSystem : IUpdate {
        public Player Player { get; }

        private readonly EntityPool<Bullet, BulletConfig> ammo1Pool;
        private readonly EntityPool<Laser, LaserConfig> ammo2Pool;

        public List<Bullet> ActiveBullets => ammo1Pool.active;
        public List<Laser> ActiveLasers => ammo2Pool.active;

        private readonly BulletConfig ammo1Config;
        private readonly LaserConfig ammo2Config;

        private bool fire1Flag;
        private bool fire2Flag;

        private float fire1Countdown;
        private float fire2Countdown;

        private float Fire1Delay => 1 / ammo1Config.fireRate;
        private float Fire2Delay => 1 / ammo2Config.fireRate;

        public delegate void FireEvent();
        public static event FireEvent Fire1Event;
        public static event FireEvent Fire2Event;

        // Laser
        private readonly LaserConfig laserConfig;

        public PlayerSystem(ConfigCollector configCollector, PrefabCollector prefabCollector) {
            // Pools
            ammo1Pool = new EntityPool<Bullet, BulletConfig>(prefabCollector.bullet, configCollector.bullet);
            ammo2Pool = new EntityPool<Laser, LaserConfig>(prefabCollector.laser, configCollector.laser);

            ammo1Config = configCollector.bullet;
            ammo2Config = configCollector.laser;

            // Player
            Player = CreatePlayer(prefabCollector.player, configCollector.player);

            // Laser
            laserConfig = configCollector.laser;

            // Subscribe
            InputController.Fire += Fire;
            InputController.Move += Player.Move;
            InputController.Rotate += Player.Rotate;

            CollisionSystem.PlayerHit += PlayerHitHandler;
        }

        private Player CreatePlayer(GameObject playerPrefab, PlayerConfig playerConfig) {
            GameObject playerObject = Object.Instantiate(playerPrefab);
            Player targetPlayer = playerObject.GetComponent<Player>();
            targetPlayer.SetData(playerConfig);
            return targetPlayer;
        }

        private void Fire(bool actionFlag, Player.Weapon weapon) {
            if (weapon == Player.Weapon.Gun) fire1Flag = actionFlag;
            else if (weapon == Player.Weapon.Laser) fire2Flag = actionFlag;
            else Debug.LogError("Weapon isn't specified!");
        }

        private void PlayerHitHandler(ICollider enemy) {
            // Player.gameObject.SetActive(false);
        }

        public void Upd(float deltaTime) {
            if (fire1Flag && fire1Countdown <= 0) {
                fire1Countdown = Fire1Delay;

                Bullet bullet = ammo1Pool.Take();
                Transform playerTransform = Player.transform;
                bullet.Set(Player.WeaponWorldPosition, playerTransform.up);
                bullet.Fire();

                Fire1Event?.Invoke();
            }

            if (fire2Flag && fire2Countdown <= 0 && Player.laserShotsCount > 0) {
                fire2Countdown = Fire2Delay;
                Player.laserShotsCount--;

                Laser laser = ammo2Pool.Take();
                Transform playerTransform = Player.transform;
                laser.Set(playerTransform.position, playerTransform.up);
                laser.Fire();

                Fire2Event?.Invoke();
            }

            if (fire1Countdown > 0) fire1Countdown -= deltaTime;
            if (fire2Countdown > 0) fire2Countdown -= deltaTime;

            // Laser
            if (Player.laserShotsCount < laserConfig.maxShotsCount) {
                if ((Player.laserShotCountdownDuration += deltaTime) >= laserConfig.shotCountdown) {
                    Player.laserShotCountdownDuration = 0;
                    Player.laserShotsCount++;
                }
            }
        }
    }
}