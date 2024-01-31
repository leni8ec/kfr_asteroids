using Framework.Objects;
using Presentation.Data;

namespace Presentation.Objects {
    public class Bullet : Ammo<BulletData>, IBullet {

        public override float Radius => data.colliderRadius;

        public void Fire() { }

        public override void Reset() { }

        private void Update() {
            transform.Translate(transform.up * data.speed);
        }

    }
}