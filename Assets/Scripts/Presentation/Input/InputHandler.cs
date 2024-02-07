using Core.State;
using Presentation.GUI;
using UnityEngine.InputSystem;

namespace Presentation.Input {
    public class InputHandler : GuiBase {

        // Player Actions
        public InputAction moveAction;
        public InputAction rotateAction;
        public InputAction fire1Action;
        public InputAction fire2Action;

        // Game Actions
        public InputAction continueAction;


        private void Awake() {
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


        private void OnFireAction(bool actionFlag, int weaponFlag, InputAction.CallbackContext context) {
            if (actionFlag) {
                States.weapon.FireState.Value |= (WeaponSystemState.Weapon)weaponFlag;
            } else {
                States.weapon.FireState.Value &= ~(WeaponSystemState.Weapon)weaponFlag;
            }
        }

        private void OnMoveAction(bool actionFlag, InputAction.CallbackContext context) {
            States.objects.player.State.MoveState.Value = actionFlag;
        }

        private void OnRotateAction(bool actionFlag, InputAction.CallbackContext context) {
            if (actionFlag) {
                States.objects.player.State.RotateState.Value = context.ReadValue<float>() < 0 ? 1 : -1;
            } else {
                States.objects.player.State.RotateState.Value = 0;
            }
        }


        private void OnContinueAction(InputAction.CallbackContext context) {
            States.game.ContinueFlag.Value = true;
        }

    }
}