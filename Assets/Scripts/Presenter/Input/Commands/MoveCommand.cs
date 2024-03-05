using Model.Data.State;
using Presenter.Input.Commands.Base;

namespace Presenter.Input.Commands {
    public class MoveCommand : CommandBase<PlayerState> {
        public MoveCommand(PlayerState state) : base(state) { }

        public void Execute(bool activeFlag) {
            State.Move.Value = activeFlag;
        }

    }
}