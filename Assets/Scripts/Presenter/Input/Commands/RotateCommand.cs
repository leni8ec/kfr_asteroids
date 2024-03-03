using Model.Data.State;
using Presenter.Input.Commands.Base;

namespace Presenter.Input.Commands {
    public class RotateCommand : CommandBase<PlayerState> {
        public RotateCommand(PlayerState state) : base(state) { }

        /// <param name="activeFlag"></param>
        /// <param name="rotateValue"> Left: -1, Right: 1 </param>
        public void Execute(bool activeFlag, float rotateValue) {
            if (activeFlag) {
                State.RotateState.Value = rotateValue < 0 ? -1 : 1;
            } else {
                State.RotateState.Value = 0;
            }
        }

    }
}