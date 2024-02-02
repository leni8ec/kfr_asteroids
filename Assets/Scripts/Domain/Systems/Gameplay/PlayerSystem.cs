using System.Collections.Generic;
using Core.Base;
using Core.Config;
using Core.Data;
using Core.Input;
using Core.Interface.Base;
using Core.Objects;
using Core.Unity;
using Domain.Systems.GameState;
using UnityEngine;

namespace Domain.Systems.Gameplay {
    public class PlayerSystem : IUpdate {
        private PlayerData Data { get; }
        public Player Player { get; }

        private EntityPool<Bullet, BulletConfig> Ammo1Pool { get; }
        private EntityPool<Laser, LaserConfig> Ammo2Pool { get; }

        public List<Bullet> ActiveBullets => Ammo1Pool.active;
        public List<Laser> ActiveLasers => Ammo2Pool.active;

        private BulletConfig Ammo1Config { get; }
        private LaserConfig Ammo2Config { get; }

        private float Fire1Delay => 1 / Ammo1Config.fireRate;
        private float Fire2Delay => 1 / Ammo2Config.fireRate;

        public delegate void FireEvent();
        public static event FireEvent Fire1Event;
        public static event FireEvent Fire2Event;

        private bool active;

        public PlayerSystem(PlayerData data, ConfigCollector configCollector, PrefabCollector prefabCollector) {
            Data = data;
            // Pools
            Ammo1Pool = new EntityPool<Bullet, BulletConfig>(prefabCollector.bullet, configCollector.bullet);
            Ammo2Pool = new EntityPool<Laser, LaserConfig>(prefabCollector.laser, configCollector.laser);

            Ammo1Config = configCollector.bullet;
            Ammo2Config = configCollector.laser;


            // Player
            Player = CreatePlayer(prefabCollector.player, configCollector.player);

            // Input listeners
            InputController.Fire += Fire;
            InputController.Move += Player.Move;
            InputController.Rotate += Player.Rotate;

            // Game state listeners
            GameStateSystem.NewGameEvent += Play;
            GameStateSystem.GameOverEvent += Reset;
        }

        private void Play() {
            active = true;
            Player.gameObject.SetActive(true);
            Data.laserShotCountdownDuration = Ammo2Config.shotRestoreCountdown;
        }

        private void Reset() {
            active = false;
            for (int i = ActiveBullets.Count - 1; i >= 0; i--) ActiveBullets[i].Reset();
            for (int i = ActiveLasers.Count - 1; i >= 0; i--) ActiveLasers[i].Reset();
            Player.Reset();
            Data.Reset();
            Player.gameObject.SetActive(false);

        }

        private Player CreatePlayer(GameObject playerPrefab, PlayerConfig playerConfig) {
            GameObject playerObject = Object.Instantiate(playerPrefab);
            Player targetPlayer = playerObject.GetComponent<Player>();
            targetPlayer.SetData(playerConfig);
            return targetPlayer;
        }

        private void Fire(bool actionFlag, Player.Weapon weapon) {
            if (weapon == Player.Weapon.Gun) Data.fire1Flag = actionFlag;
            else if (weapon == Player.Weapon.Laser) Data.fire2Flag = actionFlag;
            else Debug.LogError("Weapon isn't specified!");
        }

        public void Upd(float deltaTime) {
            if (!active) return;

            if (Data.fire1Flag && Data.fire1Countdown <= 0) {
                Data.fire1Countdown = Fire1Delay;

                Bullet bullet = Ammo1Pool.Take();
                Transform playerTransform = Player.transform;
                bullet.Set(Player.WeaponWorldPosition, playerTransform.up);
                bullet.Fire();

                Fire1Event?.Invoke();
            }

            if (Data.fire2Flag && Data.fire2Countdown <= 0 && Data.laserShotsCount > 0) {
                Data.fire2Countdown = Fire2Delay;
                Data.laserShotsCount--;

                Laser laser = Ammo2Pool.Take();
                Transform playerTransform = Player.transform;
                laser.Set(playerTransform.position, playerTransform.up);
                laser.Fire();

                Fire2Event?.Invoke();
            }

            if (Data.fire1Countdown > 0) Data.fire1Countdown -= deltaTime;
            if (Data.fire2Countdown > 0) Data.fire2Countdown -= deltaTime;

            // Laser
            if (Data.laserShotsCount < Ammo2Config.maxShotsCount) {
                if ((Data.laserShotCountdownDuration -= deltaTime) <= 0) {
                    Data.laserShotCountdownDuration = Ammo2Config.shotRestoreCountdown;
                    Data.laserShotsCount++;
                }
            }
        }
    }
}