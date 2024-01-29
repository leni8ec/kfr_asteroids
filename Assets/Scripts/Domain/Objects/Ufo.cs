using Domain.Data;
using Framework.Objects;
using UnityEngine;

namespace Domain.Objects {
    public class Ufo : Entity<UfoData>, IUfo {
        public Vector2 startPoint;
        public Vector2 startDirection;

        public Ufo(UfoData data) : base(data) { }

        public void Hunt() { }

        public void Upd(float deltaTime) { }

    }
}