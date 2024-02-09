using Model.Core.Data.State;
using Model.Core.Data.State.Base;

namespace Control.View {

    public class InputHandler {
        private StatesCollector States { get; }

        public InputHandler(StatesCollector states) {
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
            States.entity.player.State.MoveState.Value = activeFlag;
        }

        public void OnRotateAction(bool activeFlag, float value) {
            if (activeFlag) {
                States.entity.player.State.RotateState.Value = value < 0 ? -1 : 1;
            } else {
                States.entity.player.State.RotateState.Value = 0;
            }
        }

        public void OnContinueAction() {
            States.game.ContinueFlag.Value = true;
        }

    }
}