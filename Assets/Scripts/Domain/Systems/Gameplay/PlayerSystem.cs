using Domain.Base;
using Domain.Systems.Input;
using Framework.Base;
using Presentation.Data;
using Presentation.GUI;
using Presentation.Objects;
using UnityEngine;

namespace Domain.Systems.Gameplay {
    public class PlayerSystem : IUpdate {
        private readonly Player player;
        private readonly EntityPool<Bullet, BulletData> ammo1Pool;
        private readonly EntityPool<Laser, LaserData> ammo2Pool;

        private readonly BulletData ammo1Data;
        private readonly LaserData ammo2Data;

        private bool fire1Flag;
        private bool fire2Flag;

        public float fire1Countdown;
        public float fire2Countdown;

        public float Fire1Delay => 1 / ammo1Data.fireRate;
        public float Fire2Delay => 1 / ammo2Data.fireRate;


        public PlayerSystem(DataCollector dataCollector, PrefabCollector prefabCollector) {
            // Pools
            ammo1Pool = new EntityPool<Bullet, BulletData>(prefabCollector.bullet, dataCollector.bulletData);
            ammo2Pool = new EntityPool<Laser, LaserData>(prefabCollector.laser, dataCollector.laserData);

            ammo1Data = dataCollector.bulletData;
            ammo2Data = dataCollector.laserData;

            // Player
            player = CreatePlayer(prefabCollector.player, dataCollector.playerData);

            // Subscribe
            InputController.fire += Fire;
            InputController.move += player.Move;
            InputController.rotate += player.Rotate;
        }

        private Player CreatePlayer(GameObject playerPrefab, PlayerData playerData) {
            GameObject playerObject = Object.Instantiate(playerPrefab);
            Player targetPlayer = playerObject.GetComponent<Player>();
            targetPlayer.SetData(playerData);
            return targetPlayer;
        }

        private void Fire(bool actionFlag, Player.Weapon weapon) {
            if (weapon == Player.Weapon.Gun) fire1Flag = actionFlag;
            else if (weapon == Player.Weapon.Laser) fire2Flag = actionFlag;
            else Debug.LogError("Weapon isn't specified!");
        }

        public void Upd(float deltaTime) {
            if (fire1Flag && fire1Countdown <= 0) {
                fire1Countdown = Fire1Delay;

                Bullet bullet = ammo1Pool.Take();
                Transform playerTransform = player.transform;
                bullet.Set(player.WeaponWorldPosition, playerTransform.up);
                bullet.Fire();
            }

            if (fire2Flag && fire2Countdown <= 0) {
                fire2Countdown = Fire2Delay;

                Laser laser = ammo2Pool.Take();
                Transform transform = player.transform;
                laser.Set(transform.position, transform.up);
                laser.Fire();
            }

            if (fire1Countdown > 0) fire1Countdown -= deltaTime;
            if (fire2Countdown > 0) fire2Countdown -= deltaTime;
        }
    }
}