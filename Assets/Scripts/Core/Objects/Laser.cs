using Core.Config;
using Core.Interface.Objects;
using UnityEngine;

namespace Core.Objects {
    public class Laser : Ammo<LaserConfig>, ILaser {
        public Transform scaledTransform;
        public SpriteRenderer laserSprite;

        public override float Radius => data.colliderRadius;
        public Vector2 Direction => direction;
        public float MaxDistance => data.maxDistance;
        public int MaxShotsCount => data.maxShotsCount;

        private float duration;

        public void Fire() {
            duration = data.duration;

            transform.up = direction;

            // Set laser scale (visual)
            Vector3 scale = scaledTransform.localScale;
            scale.y = data.maxDistance;
            scaledTransform.localScale = scale;
        }

        private void Update() {
            if ((duration -= Time.deltaTime) < 0) {
                Reset();
            }
            Color color = laserSprite.color;
            color.a = Mathf.Max(0, duration / data.duration);
            laserSprite.color = color;
        }

    }
}