using Control.Input.Commands.Base;
using Model.Core.Data.State;

namespace Control.Input.Commands {
    public class ContinueCommand : CommandBase<GameSystemState> {
        public ContinueCommand(GameSystemState state) : base(state) { }

        public void Execute() {
            State.ContinueFlag.Value = true;
        }

    }
}