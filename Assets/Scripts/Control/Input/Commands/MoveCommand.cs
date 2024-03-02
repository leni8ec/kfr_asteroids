using Control.Input.Commands.Base;
using Model.Core.Data.State;

namespace Control.Input.Commands {
    public class MoveCommand : CommandBase<PlayerState> {
        public MoveCommand(PlayerState state) : base(state) { }

        public void Execute(bool activeFlag) {
            State.MoveState.Value = activeFlag;
        }

    }
}