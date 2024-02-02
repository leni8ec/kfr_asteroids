﻿using System;
using Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input {
    public class InputController : MonoBehaviour {

        public delegate void FireEvent(bool actionFlag, WeaponState.Weapon weapon);
        public delegate void RotateEvent(bool actionFlag, bool left);
        public delegate void MoveEvent(bool actionFlag);

        public static event FireEvent Fire;
        public static event RotateEvent Rotate;
        public static event MoveEvent Move;

        public InputAction moveAction;
        public InputAction rotateAction;
        public InputAction fire1Action;
        public InputAction fire2Action;

        public InputAction continueAction;
        public static event Action Continue;


        private void Awake() {
            moveAction.performed += context => OnMoveAction(true, context);
            rotateAction.performed += context => OnRotateAction(true, context);
            fire1Action.performed += context => OnFire1Action(true, context);
            fire2Action.performed += context => OnFire2Action(true, context);

            moveAction.canceled += context => OnMoveAction(false, context);
            rotateAction.canceled += context => OnRotateAction(false, context);
            fire1Action.canceled += context => OnFire1Action(false, context);
            fire2Action.canceled += context => OnFire2Action(false, context);

            continueAction.performed += OnContinueAction;
        }


        private void OnEnable() {
            moveAction.Enable();
            rotateAction.Enable();
            fire1Action.Enable();
            fire2Action.Enable();
            continueAction.Enable();
        }

        private void OnDisable() {
            moveAction.Disable();
            rotateAction.Disable();
            fire1Action.Disable();
            fire2Action.Disable();
            continueAction.Disable();
        }


        private void OnMoveAction(bool actionFlag, InputAction.CallbackContext context) {
            Move(actionFlag);
        }

        private void OnRotateAction(bool actionFlag, InputAction.CallbackContext context) {
            Rotate(actionFlag, context.ReadValue<float>() < 0);
        }

        private void OnFire1Action(bool actionFlag, InputAction.CallbackContext context) {
            Fire(actionFlag, WeaponState.Weapon.Gun);
        }

        private void OnFire2Action(bool actionFlag, InputAction.CallbackContext context) {
            Fire(actionFlag, WeaponState.Weapon.Laser);
        }


        private void OnContinueAction(InputAction.CallbackContext context) {
            Continue?.Invoke();
        }

    }
}