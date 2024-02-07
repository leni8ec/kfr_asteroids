using Model.Core.State;

namespace Control.Input {

    public class InputHandler {
        private StateCollector States { get; }

        public InputHandler(StateCollector states) {
            States = states;
        }

        public void OnFire(bool activeFlag, int weaponNumber) {
            WeaponSystemState.Weapon weapon = weaponNumber == 1
                ? WeaponSystemState.Weapon.Gun
                : WeaponSystemState.Weapon.Laser;

            if (activeFlag) {
                States.weapon.FireState.Value |= weapon;
            } else {
                States.weapon.FireState.Value &= ~weapon;
            }
        }

        public void OnMoveAction(bool activeFlag) {
            States.objects.player.State.MoveState.Value = activeFlag;
        }

        public void OnRotateAction(bool activeFlag, float value) {
            if (activeFlag) {
                States.objects.player.State.RotateState.Value = value < 0 ? -1 : 1;
            } else {
                States.objects.player.State.RotateState.Value = 0;
            }
        }

        public void OnContinueAction() {
            States.game.ContinueFlag.Value = true;
        }

    }
}