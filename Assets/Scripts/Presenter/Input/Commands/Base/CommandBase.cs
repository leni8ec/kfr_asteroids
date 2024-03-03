using Model.Data.State.Base;

namespace Presenter.Input.Commands.Base {
    public abstract class CommandBase<TState> : ICommand where TState : IStateData {
        protected TState State { get; }

        protected CommandBase(TState state) {
            State = state;
        }

    }
}