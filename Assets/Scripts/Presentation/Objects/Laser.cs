using Framework.Objects;
using Presentation.Data;

namespace Presentation.Objects {
    public class Laser : Ammo<LaserData>, ILaser {

        public override float Radius => data.colliderRadius;

        public void Fire() { }

        public override void Reset() { }

    }
}