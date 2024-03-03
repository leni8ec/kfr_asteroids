using UnityEngine;

namespace Model.Core.Entity.Interface {
    public interface ICollider {
        public float ColliderRadius { get; }
        public Vector3 Pos { get; }
    }
}