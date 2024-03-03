using Presenter.Input;
using UnityEngine.InputSystem;
using UnityView.Base;

namespace UnityView.Input {
    public class UnityInputListener : MonoBase {

        // Player Actions
        public InputAction moveAction;
        public InputAction rotateAction;
        public InputAction fire1Action;
        public InputAction fire2Action;

        // Game Actions
        public InputAction continueAction;

        private InputHandler handler;

        private void Start() {
            handler = new InputHandler(States);

            moveAction.performed += context => OnMoveAction(true, context);
            rotateAction.performed += context => OnRotateAction(true, context);
            fire1Action.performed += context => OnFireAction(true, 1 << 0, context);
            fire2Action.performed += context => OnFireAction(true, 2 << 0, context);

            moveAction.canceled += context => OnMoveAction(false, context);
            rotateAction.canceled += context => OnRotateAction(false, context);
            fire1Action.canceled += context => OnFireAction(false, 1 << 0, context);
            fire2Action.canceled += context => OnFireAction(false, 2 << 0, context);

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


        private void OnFireAction(bool actionFlag, int weaponNumber, InputAction.CallbackContext context) {
            handler.FireCommand.Execute(actionFlag, weaponNumber);
        }

        private void OnMoveAction(bool actionFlag, InputAction.CallbackContext context) {
            handler.MoveCommand.Execute(actionFlag);
        }

        private void OnRotateAction(bool actionFlag, InputAction.CallbackContext context) {
            handler.RotateCommand.Execute(actionFlag, -context.ReadValue<float>()); // send inversion value of rotation
        }

        private void OnContinueAction(InputAction.CallbackContext context) {
            handler.ContinueCommand.Execute();
        }

    }
}