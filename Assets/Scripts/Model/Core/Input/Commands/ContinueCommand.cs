using Model.Core.Data.State;
using Model.Core.Input.Commands.Base;

namespace Model.Core.Input.Commands {
    public class ContinueCommand : CommandBase<GameSystemState> {
        public ContinueCommand(GameSystemState state) : base(state) { }

        public void Execute() {
            State.ContinueFlag.Value = true;
        }

    }
}