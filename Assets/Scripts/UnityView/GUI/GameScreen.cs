using Model.Core.Data.State;
using Model.Core.Entity;
using TMPro;
using UnityEngine;
using UnityView.Base;

namespace UnityView.GUI {
    public class GameScreen : MonoBase {
        public TextMeshProUGUI points;
        public TextMeshProUGUI coords;
        public TextMeshProUGUI angle;
        public TextMeshProUGUI speed;
        public TextMeshProUGUI laserCount;
        public TextMeshProUGUI laserCountdown;

        private EntitiesState entitiesState;
        private ScoreSystemState scoreSystemState;
        private WeaponSystemState weaponSystemState;

        private void Start() {
            entitiesState = States.Get<EntitiesState>();
            scoreSystemState = States.Get<ScoreSystemState>();
            weaponSystemState = States.Get<WeaponSystemState>();

            scoreSystemState.Points.Changed += score => points.SetText($"Score: {score}");
        }

        private void Update() {
            Player player = entitiesState.player;
            if (player == null) return; // if player doesn't initialized

            Transform playerTransform = player.Transform;
            Vector3 playerPosition = playerTransform.position;

            coords.SetText($"Coords: [{playerPosition.x:F1}:{playerPosition.y:F1}]");
            angle.SetText($"Angle: {playerTransform.eulerAngles.z:F0}");
            speed.SetText($"Speed: {entitiesState.player.State.speed:N}");
            laserCount.SetText($"Laser Count: {weaponSystemState.laserShotsCount}");
            laserCountdown.SetText($"Laser Countdown: {weaponSystemState.laserShotCountdownDuration:0.00}");
        }

    }
}