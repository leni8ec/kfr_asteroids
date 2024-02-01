using Framework.Objects;
using Presentation.Data;
using UnityEngine;

namespace Presentation.Objects {
    public class Asteroid : Enemy<AsteroidData>, IAsteroid {

        public override float Radius => data.colliderRadius;

        private void Update() {
            transform.Translate(direction * (data.speed * Time.deltaTime));
        }

    }
}