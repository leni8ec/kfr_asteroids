using Core.Config;
using Core.Interface.Objects;
using Core.Objects.Base;
using Core.State;

namespace Core.Objects {
    public class Asteroid : Enemy<AsteroidState, AsteroidConfig>, IAsteroid {
        public delegate void ExplosionEvent(Asteroid asteroid);
        public static event ExplosionEvent Explosion;

        public float DestroyedFragments => Config.destroyFragments;
        public AsteroidConfig.Size Size => Config.size;


        protected override void Initialize() { }

        public override void Reset() { }

        public override void Upd(float deltaTime) {
            Transform.Translate(State.Direction * (Config.speed * deltaTime));
        }

        public override void Destroy() {
            base.Destroy();
            Explosion?.Invoke(this);
        }

    }
}