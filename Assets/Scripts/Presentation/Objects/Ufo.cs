using Framework.Objects;
using Presentation.Data;
using UnityEngine;

namespace Presentation.Objects {
    public class Ufo : Entity<UfoData>, IUfo {
        public Vector2 startPoint;
        public Vector2 startDirection;

        public void Hunt(Transform target) { }

        public override float Radius => data.colliderRadius;

        public override void Reset() { }
    }
}