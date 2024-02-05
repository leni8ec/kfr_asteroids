using Core.Config;
using Core.Interface.Objects;
using Core.State;
using UnityEngine;

namespace Core.Objects {
    public class Laser : Ammo<LaserAmmoState, LaserConfig>, ILaser {
        [SerializeField] private Transform scaledTransform;
        [SerializeField] private SpriteRenderer laserSprite;

        public float MaxDistance => Config.maxDistance;

        protected override void Initialize() { }

        public void Fire() {
            State.duration = Config.duration;

            transform.up = State.Direction;

            // Set laser scale (visual)
            Vector3 scale = scaledTransform.localScale;
            scale.y = Config.maxDistance;
            scaledTransform.localScale = scale;
        }

        private void Update() {
            if ((State.duration -= Time.deltaTime) < 0) {
                Destroy();
            }
            Color color = laserSprite.color;
            color.a = Mathf.Max(0, State.duration / Config.duration);
            laserSprite.color = color;
        }

    }
}