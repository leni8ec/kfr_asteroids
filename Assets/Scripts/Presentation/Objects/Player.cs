using Framework.Objects;
using Presentation.Data;
using UnityEngine;

namespace Presentation.Objects {
    public class Player : Entity<PlayerData>, IPlayer {
        private bool moveFlag;
        private bool isMoving;

        private bool rotateFlag;
        private int rotateDirection;

        [Space]
        public SpriteRenderer spriteRenderer;
        public Sprite idleSprite;
        public Sprite moveSprite;

        public override float Radius => data.colliderRadius;

        public void Rotate(bool actionFlag, bool left) {
            rotateFlag = actionFlag;
            rotateDirection = left ? 1 : -1;
        }

        // Move must have inertia and the screen - is infinity

        public void Move(bool actionFlag) {
            moveFlag = actionFlag;
        }


        public enum Weapon {
            Gun = 0,
            Laser = 1,
        }

        public override void Reset() { }

        private void Update() {
            float deltaTime = Time.deltaTime;

            if (moveFlag) {
                // transform.Translate(transform.up * (data.speed * deltaTime));
                Vector3 position = transform.position;
                position += transform.up * (data.speed * deltaTime);
                transform.position = position;

                if (!isMoving) {
                    spriteRenderer.sprite = moveSprite;
                    isMoving = true;
                }
            } else {
                if (isMoving) {
                    spriteRenderer.sprite = idleSprite;
                    isMoving = false;
                }
            }

            if (rotateFlag) {
                transform.Rotate(0, 0, data.rotationSpeed * deltaTime * rotateDirection);
            }

        }
    }
}