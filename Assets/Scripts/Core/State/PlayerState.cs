using Core.Base;
using UnityEngine;

namespace Core.State {
    public class PlayerState : IStateData {

        #region Input state

        /// <summary>
        /// States (bool):
        /// <para> 'false' - Idle </para>
        /// <para> 'true' - Movement </para>
        /// </summary>
        public ValueChange<bool> MoveState { get; } = new();
        /// <summary>
        /// States (int):
        /// <para> 0 - Empty </para>
        /// <para> -1 - Right </para>
        /// <para> 1 - Left </para>
        /// </summary>
        public ValueChange<int> RotateState { get; } = new();

        #endregion


        public float inertialSpeed;
        public float inertialTime;

        public Vector3 lastDirection;
        public Vector3 lastPos;

        public float speed;

        public void Reset() {
            MoveState.Reset();
            RotateState.Reset();
            inertialSpeed = 0;
            inertialTime = 0;
            lastDirection = Vector3.zero;
            lastPos = Vector3.zero;
            speed = 0;
        }

    }
}