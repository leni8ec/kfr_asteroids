using Model.Core.Data.State;
using Model.Core.Input.Commands.Base;

namespace Model.Core.Input.Commands {
    public class MoveCommand : CommandBase<PlayerState> {
        public MoveCommand(PlayerState state) : base(state) { }

        public void Execute(bool activeFlag) {
            State.MoveState.Value = activeFlag;
        }

    }
}