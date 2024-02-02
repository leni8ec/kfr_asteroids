using Core.Objects;
using TMPro;
using UnityEngine;

namespace Presentation.GUI {
    public class GameScreen : GuiBase {
        public TextMeshProUGUI points;
        public TextMeshProUGUI coords;
        public TextMeshProUGUI angle;
        public TextMeshProUGUI speed;
        public TextMeshProUGUI laserCount;
        public TextMeshProUGUI laserCountdown;

        private void Start() {
            State.Score.Points.Changed += score => points.SetText($"Score: {score}");
        }

        private void Update() {
            Player player = SceneData.Player;
            Transform playerTransform = player.transform;
            Vector3 playerPosition = playerTransform.position;

            coords.SetText($"Coords: [{playerPosition.x:F1}:{playerPosition.y:F1}]");
            angle.SetText($"Angle: {playerTransform.eulerAngles.z:F0}");
            speed.SetText($"Speed: {player.Speed:N}");
            laserCount.SetText($"Laser Count: {State.Player.laserShotsCount}");
            laserCountdown.SetText($"Laser Countdown: {State.Player.laserShotCountdownDuration:0.00}");
        }

    }
}