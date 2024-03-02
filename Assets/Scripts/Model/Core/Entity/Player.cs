using Model.Core.Data.State;
using Model.Core.Data.Unity.Config;
using Model.Core.Entity.Base;
using Model.Core.Interface.Entity;
using UnityEngine;

namespace Model.Core.Entity {
    public class Player : ColliderEntity<PlayerState, PlayerConfig>, IPlayer {

        public Vector3 WeaponWorldPosition => Transform.position + Transform.up * 0.2f;


        public override void Upd(float deltaTime) {
            // Moving
            if (State.MoveState.Value) {
                if (State.inertialTime < 1) {
                    State.inertialTime = Mathf.Min(1, State.inertialTime + deltaTime * (1 / Config.accelerationInertia));
                    State.inertialSpeed = Mathf.Lerp(0, Config.speed, State.inertialTime);
                }
            } else {
                if (State.inertialTime > 0) {
                    State.inertialTime = Mathf.Max(0, State.inertialTime - deltaTime * (1 / Config.brakingInertia));
                    State.inertialSpeed = Mathf.Lerp(0, Config.speed, State.inertialTime);
                }
            }

            if (State.inertialTime > 0) {
                // transform.Translate(transform.up * (config.speed * deltaTime));
                Vector3 direction;
                if (State.MoveState.Value) {
                    direction = Vector3.Lerp(State.lastDirection, Transform.up, deltaTime / Config.leftOverInertia); // leftover inertia
                } else {
                    direction = State.lastDirection; // don't change direction without acceleration
                }
                State.lastDirection = direction * State.inertialTime;

                Vector3 position = Transform.position;
                position += direction * (State.inertialSpeed * deltaTime);
                Transform.position = position;
            }


            // Rotation
            if (State.RotateState.Value != 0) {
                Transform.Rotate(0, 0, Config.rotationSpeed * deltaTime * State.RotateState.Value);
            }

            // Calculate speed
            Vector3 pos = Transform.position;
            State.speed = Vector3.Distance(State.lastPos, pos) / deltaTime;
            State.lastPos = pos;

        }

    }
}