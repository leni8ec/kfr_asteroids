using Core.Config;
using Core.Interface.Objects;
using Core.Objects.Base;
using Core.State;
using UnityEngine;
using Random = System.Random;

namespace Core.Objects {
    public class Asteroid : Enemy<AsteroidState, AsteroidConfig>, IAsteroid {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] sprites;

        public delegate void ExplosionEvent(Asteroid asteroid);
        public static event ExplosionEvent Explosion;

        public float DestroyedFragments => Config.destroyFragments;
        public AsteroidConfig.Size Size => Config.size;

        private void Start() {
            int index = new Random().Next(sprites.Length);
            spriteRenderer.sprite = sprites[index];
        }

        protected override void Initialize() { }

        public override void Reset() { }

        private void Update() {
            Transform t = transform;
            t.Translate(State.Direction * (Config.speed * Time.deltaTime));
        }

        public override void Destroy() {
            base.Destroy();
            Explosion?.Invoke(this);
        }


    }
}