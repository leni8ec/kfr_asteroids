using Framework.Objects;
using Presentation.Data;
using UnityEngine;

namespace Presentation.Objects {
    public class Ufo : Enemy<UfoData>, IUfo {

        public void Hunt(Transform target) { }

        public override float Radius => data.colliderRadius;

    }
}