using Core.Config;
using Core.Interface.Objects;
using UnityEngine;
using Random = System.Random;

namespace Core.Objects {
    public class Asteroid : Enemy<AsteroidConfig>, IAsteroid {
        public Size size;
        [Space]
        public SpriteRenderer spriteRenderer;
        public Sprite[] sprites;

        public delegate void ExplosionEvent(Asteroid asteroid);
        public static event ExplosionEvent Explosion;

        public float DestroyedFragments => config.destroyFragments;
        public override float Radius => config.colliderRadius;

        public float Lifetime { get; private set; }

        private void Start() {
            int index = new Random().Next(sprites.Length);
            spriteRenderer.sprite = sprites[index];
        }

        private void Update() {
            Transform t = transform;
            t.Translate(direction * (config.speed * Time.deltaTime));

            Lifetime += Time.deltaTime;
        }

        public override void Reset() {
            base.Reset();
            Lifetime = 0;
        }

        public override void Destroy() {
            base.Destroy();
            Explosion?.Invoke(this);
        }

        public enum Size {
            Large,
            Medium,
            Small
        }

    }
}