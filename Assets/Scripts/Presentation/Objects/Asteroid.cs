using Framework.Objects;
using Presentation.Data;
using UnityEngine;
using Random = System.Random;

namespace Presentation.Objects {
    public class Asteroid : Enemy<AsteroidData>, IAsteroid {
        public Size size;
        [Space]
        public SpriteRenderer spriteRenderer;
        public Sprite[] sprites;

        public delegate void ExplosionEvent(Size size);
        public static event ExplosionEvent Explosion;

        public override float Radius => data.colliderRadius;

        public float Lifetime { get; private set; }

        private void Start() {
            int index = new Random().Next(sprites.Length);
            spriteRenderer.sprite = sprites[index];
        }

        private void Update() {
            Transform t = transform;
            t.Translate(direction * (data.speed * Time.deltaTime));

            Lifetime += Time.deltaTime;
        }

        public override void Reset() {
            base.Reset();
            Lifetime = 0;
        }

        public override void Destroy() {
            base.Destroy();
            Explosion?.Invoke(size);
        }

        public enum Size {
            Large,
            Medium,
            Small
        }

    }
}