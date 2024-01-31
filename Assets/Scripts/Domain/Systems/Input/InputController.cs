using Presentation.Objects;

namespace Domain.Systems.Input {
    public class InputController {

        public delegate void Fire(Player.Weapon weapon);
        public delegate void Rotate(bool left);
        public delegate void Move();

        public static Fire fire;
        public static Rotate rotate;
        public static Move move;


        public void FireAction(Player.Weapon weapon) {
            fire(weapon);
        }

        public void RotateAction(bool left) {
            rotate(left);
        }

        public void MoveAction() {
            move();
        }

    }
}