using Presentation.Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Domain.Systems.Input {
    public class InputController : MonoBehaviour {

        public static Fire fire;
        public static Rotate rotate;
        public static Move move;

        public delegate void Fire(bool actionFlag, Player.Weapon weapon);
        public delegate void Rotate(bool actionFlag, bool left);
        public delegate void Move(bool actionFlag);

        public InputAction moveAction;
        public InputAction rotateAction;
        public InputAction fire1Action;
        public InputAction fire2Action;

        private void Awake() {
            moveAction.performed += context => OnMoveAction(true, context);
            rotateAction.performed += context => OnRotateAction(true, context);
            fire1Action.performed += context => OnFire1Action(true, context);
            fire2Action.performed += context => OnFire2Action(true, context);

            moveAction.canceled += context => OnMoveAction(false, context);
            rotateAction.canceled += context => OnRotateAction(false, context);
            fire1Action.canceled += context => OnFire1Action(false, context);
            fire2Action.canceled += context => OnFire2Action(false, context);
        }

        private void OnEnable() {
            moveAction.Enable();
            rotateAction.Enable();
            fire1Action.Enable();
            fire2Action.Enable();
        }

        private void OnDisable() {
            moveAction.Disable();
            rotateAction.Disable();
            fire1Action.Disable();
            fire2Action.Disable();
        }


        private void OnMoveAction(bool actionFlag, InputAction.CallbackContext context) {
            move(actionFlag);
        }

        private void OnRotateAction(bool actionFlag, InputAction.CallbackContext context) {
            rotate(actionFlag, context.ReadValue<float>() < 0);
        }

        private void OnFire1Action(bool actionFlag, InputAction.CallbackContext context) {
            Debug.Log("[Input] OnFire1Action");
            fire(actionFlag, Player.Weapon.Gun);
        }

        private void OnFire2Action(bool actionFlag, InputAction.CallbackContext context) {
            Debug.Log("[Input] OnFire2Action");
            fire(actionFlag, Player.Weapon.Laser);
        }

    }
}