using Framework.Objects;
using Presentation.Data;
using Presentation.GUI;
using UnityEngine;

namespace Presentation.Objects {
    public class Asteroid : Enemy<AsteroidData>, IAsteroid {

        public override float Radius => data.colliderRadius;

        public float Lifetime { get; private set; }

        private void Update() {
            Transform t = transform;
            t.Translate(direction * (data.speed * Time.deltaTime));

            Lifetime += Time.deltaTime;
        }

        public override void Reset() {
            base.Reset();
            Lifetime = 0;
        }

    }
}