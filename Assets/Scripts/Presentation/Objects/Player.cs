using Framework.Objects;
using Presentation.Data;
using UnityEngine;

namespace Presentation.Objects {
    public class Player : Entity<PlayerData>, IPlayer {
        private bool moveFlag;
        private bool isMoving;

        private bool rotateFlag;
        private int rotateDirection;

        private float inertialSpeed;
        private float inertialTime;

        private Vector3 lastDirection;

        [Space]
        public SpriteRenderer spriteRenderer;
        public Sprite idleSprite;
        public Sprite moveSprite;

        public override float Radius => data.colliderRadius;

        // position of bullet start
        public Vector3 WeaponWorldPosition => transform.position + transform.up * 0.2f;

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

        private void Update() {
            float deltaTime = Time.deltaTime;

            // Moving
            if (moveFlag) {
                if (inertialTime < 1) {
                    inertialTime = Mathf.Min(1, inertialTime + deltaTime * (1 / data.accelerationInertia));
                    inertialSpeed = Mathf.Lerp(0, data.speed, inertialTime);
                }
                if (!isMoving) {
                    isMoving = true;
                    spriteRenderer.sprite = moveSprite;
                }
            } else {
                if (inertialTime > 0) {
                    inertialTime = Mathf.Max(0, inertialTime - deltaTime * (1 / data.brakingInertia));
                    inertialSpeed = Mathf.Lerp(0, data.speed, inertialTime);
                }
                if (isMoving) {
                    isMoving = false;
                    spriteRenderer.sprite = idleSprite;
                }
            }

            if (inertialTime > 0) {
                // transform.Translate(transform.up * (data.speed * deltaTime));
                Transform t = transform;
                Vector3 direction;
                if (moveFlag) direction = Vector3.Lerp(lastDirection, t.up, deltaTime / data.leftOverInertia); // leftover inertia
                else direction = lastDirection; // don't change direction without acceleration
                lastDirection = direction;

                Vector3 position = t.position;
                position += direction * (inertialSpeed * deltaTime);
                t.position = position;
            }

            // Rotation
            if (rotateFlag) {
                transform.Rotate(0, 0, data.rotationSpeed * deltaTime * rotateDirection);
            }

        }
    }
}