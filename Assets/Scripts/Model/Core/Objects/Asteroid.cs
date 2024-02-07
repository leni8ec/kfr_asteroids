using Model.Core.Config;
using Model.Core.Interface.Objects;
using Model.Core.Objects.Base;
using Model.Core.State;

namespace Model.Core.Objects {
    public class Asteroid : Enemy<AsteroidState, AsteroidConfig>, IAsteroid {
        public delegate void ExplosionEvent(Asteroid asteroid);
        public static event ExplosionEvent Explosion;

        public float DestroyedFragments => Config.destroyFragments;
        public AsteroidConfig.Size Size => Config.size;


        protected override void Initialize() { }

        protected override void OnReset() { }

        public override void Upd(float deltaTime) {
            Transform.Translate(State.Direction * (Config.speed * deltaTime));
        }

        public override void Destroy() {
            Explosion?.Invoke(this); // Explosion must be before Destroy!
            base.Destroy();
        }

    }
}