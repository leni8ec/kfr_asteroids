using Model.Core.Data.State;
using Model.Core.Input.Commands.Base;

namespace Model.Core.Input.Commands {
    public class FireCommand : CommandBase<WeaponSystemState> {

        public FireCommand(WeaponSystemState state) : base(state) { }

        public void Execute(bool activeFlag, int weaponNumber) {
            WeaponSystemState.Weapon weapon = weaponNumber == 1
                ? WeaponSystemState.Weapon.Gun
                : WeaponSystemState.Weapon.Laser;

            if (activeFlag) {
                State.fireStatus |= weapon;
            } else {
                State.fireStatus &= ~weapon;
            }
        }

    }
}