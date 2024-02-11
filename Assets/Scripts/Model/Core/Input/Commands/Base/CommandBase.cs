using Model.Core.Interface.State;

namespace Model.Core.Input.Commands.Base {
    public abstract class CommandBase<TState> : ICommand where TState : IStateData {
        protected TState State { get; }

        protected CommandBase(TState state) {
            State = state;
        }

    }
}