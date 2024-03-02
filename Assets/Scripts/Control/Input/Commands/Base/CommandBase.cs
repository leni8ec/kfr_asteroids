using Model.Core.Data.State.Base;

namespace Control.Input.Commands.Base {
    public abstract class CommandBase<TState> : ICommand where TState : IStateData {
        protected TState State { get; }

        protected CommandBase(TState state) {
            State = state;
        }

    }
}