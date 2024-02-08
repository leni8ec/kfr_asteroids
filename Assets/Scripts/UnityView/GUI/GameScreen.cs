using Model.Core.Objects.Game;
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

        private void Start() {
            States.score.Points.Changed += score => points.SetText($"Score: {score}");
        }

        private void Update() {
            Player player = SceneData.States.objects.player;
            if (player == null) return; // if player doesn't initialized

            Transform playerTransform = player.Transform;
            Vector3 playerPosition = playerTransform.position;

            coords.SetText($"Coords: [{playerPosition.x:F1}:{playerPosition.y:F1}]");
            angle.SetText($"Angle: {playerTransform.eulerAngles.z:F0}");
            speed.SetText($"Speed: {States.objects.player.State.speed:N}");
            laserCount.SetText($"Laser Count: {States.weapon.laserShotsCount}");
            laserCountdown.SetText($"Laser Countdown: {States.weapon.laserShotCountdownDuration:0.00}");
        }

    }
}