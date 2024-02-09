using Model.Core.Data.State;
using Model.Core.Entity.Base;
using Model.Core.Interface.Entity;
using Model.Core.Unity.Data.Config;

namespace Model.Core.Entity {
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