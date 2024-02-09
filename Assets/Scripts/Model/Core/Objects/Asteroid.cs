using Model.Core.Data.State;
using Model.Core.Interface.Objects;
using Model.Core.Objects.Base;
using Model.Core.Unity.Data.Config;

namespace Model.Core.Objects {
    public class Asteroid : Enemy<AsteroidState, AsteroidConfig>, IAsteroid {
        public delegate void ExplosionEventHandler(Asteroid asteroid);
        public static event ExplosionEventHandler ExplosionEvent;

        public float DestroyedFragments => Config.destroyFragments;
        public AsteroidConfig.Size Size => Config.size;


        public override void Upd(float deltaTime) {
            Transform.Translate(State.Direction * (Config.speed * deltaTime));
        }

        protected override void OnDestroy() {
            ExplosionEvent?.Invoke(this);
        }

    }
}