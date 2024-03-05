using Model.Data.Reactive;
using Model.Data.State.Base;
using UnityEngine;

namespace Model.Data.State {
    public class PlayerState : EntityState {

        #region Input state

        /// <summary>
        /// States (bool):
        /// <para> 'false' - Idle </para>
        /// <para> 'true' - Movement </para>
        /// </summary>
        public ReactiveProperty<bool> Move { get; } = new();
        /// <summary>
        /// States (int):
        /// <para> 0 - Empty </para>
        /// <para> -1 - Right </para>
        /// <para> 1 - Left </para>
        /// </summary>
        public ReactiveProperty<int> Rotate { get; } = new();

        #endregion


        public float inertialSpeed;
        public float inertialTime;

        public Vector3 lastDirection;
        public Vector3 lastPos;

        public float speed;

        public Vector3 weaponWorldPosition;


        protected override void OnReset() {
            Move.Reset();
            Rotate.Reset();
            inertialSpeed = default;
            inertialTime = default;
            lastDirection = default;
            lastPos = default;
            speed = default;
            weaponWorldPosition = default;
        }

    }
}