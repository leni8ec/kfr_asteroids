using Framework.Objects;
using Presentation.Data;

namespace Presentation.Objects {
    public class Asteroid : Entity<AsteroidData>, IAsteroid {
        public float direction;

        public override float Radius => data.colliderRadius;

        public override void Reset() { }
    }
}