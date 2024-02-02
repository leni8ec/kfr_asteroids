using UnityEngine;

namespace Core.Interface.Objects {
    public interface ICollider {
        public float Radius { get; }
        public Vector2 Pos { get; }

    }
}