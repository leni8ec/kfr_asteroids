using Framework.Objects;
using Presentation.Data;

namespace Presentation.Objects {
    public class Asteroid : Enemy<AsteroidData>, IAsteroid {

        public override float Radius => data.colliderRadius;

    }
}