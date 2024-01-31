using Domain.Base;
using Domain.Systems.Input;
using Presentation.Data;
using Presentation.GUI;
using Presentation.Objects;
using UnityEngine;

namespace Domain.Systems.Gameplay {
    public class PlayerSystem {
        private readonly Player player;
        private readonly EntityPool<Bullet, BulletData> bulletPool;
        private readonly EntityPool<Laser, LaserData> laserPool;

        public PlayerSystem(DataCollector dataCollector, PrefabCollector prefabCollector) {
            // Pools
            bulletPool = new EntityPool<Bullet, BulletData>(prefabCollector.bullet, dataCollector.bulletData);
            laserPool = new EntityPool<Laser, LaserData>(prefabCollector.laser, dataCollector.laserData);

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
            if (weapon == Player.Weapon.Gun) FireBullet();
            else if (weapon == Player.Weapon.Laser) FireLaser();
            else Debug.LogError("Weapon isn't specified!");
        }

        private void FireBullet() {
            Bullet bullet = bulletPool.Take();
            Transform transform = player.transform;
            bullet.Set(transform.position, transform.up);
            bullet.Fire();
        }

        private void FireLaser() {
            Laser laser = laserPool.Take();
            Transform transform = player.transform;
            laser.Set(transform.position, transform.up);
            laser.Fire();
        }

    }
}