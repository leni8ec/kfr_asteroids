using Model.Data.State;
using Presenter.Input.Commands.Base;

namespace Presenter.Input.Commands {
    public class ContinueCommand : CommandBase<GameSystemState> {
        public ContinueCommand(GameSystemState state) : base(state) { }

        public void Execute() {
            State.ContinueFlag.Value = true;
        }

    }
}