using Framework.Objects;
using Presentation.Data;
using UnityEngine;

namespace Presentation.Objects {
    public class Player : Entity<PlayerData>, IPlayer {
        private int rotateFlag;
        private bool moveFlag;

        public override float Radius => data.colliderRadius;

        public void Rotate(bool left) {
            rotateFlag = left ? -1 : 1;
        }

        // Move must have inertia and the screen - is infinity

        public void Move() {
            moveFlag = true;
        }


        public enum Weapon {
            Gun = 0,
            Laser = 1,
        }

        public override void Reset() { }

        private void Update() {
            float deltaTime = Time.deltaTime;

            if (moveFlag) {
                transform.Translate(transform.up * (data.speed * deltaTime));
                moveFlag = false;
            }

            if (rotateFlag != 0) {
                transform.Rotate(0, 0, data.rotationSpeed * deltaTime);
                rotateFlag = 0;
            }

        }
    }
}