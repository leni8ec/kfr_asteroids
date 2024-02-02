using Core.Base;
using UnityEngine;

namespace Core.State {
    public class PlayerState : IStateData {
        public ValueChange<bool> MoveFlag { get; } = new();

        public ValueChange<bool> RotateFlag { get; } = new();
        public int rotateDirection;

        public float inertialSpeed;
        public float inertialTime;

        public Vector3 lastDirection;
        public Vector3 lastPos;

        public float speed;

        public void Reset() { }

    }
}