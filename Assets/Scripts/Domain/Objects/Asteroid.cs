using Domain.Data;
using Framework.Objects;

namespace Domain.Objects {
    public class Asteroid : Entity<AsteroidData>, IAsteroid {
        public float direction;

        public Asteroid(AsteroidData data) : base(data) { }

        public void Upd(float deltaTime) { }

    }
}