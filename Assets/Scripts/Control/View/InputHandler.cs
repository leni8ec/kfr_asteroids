using Model.Core.Data.State.Base;
using Model.Core.Input.Commands;

namespace Control.View {

    public class InputHandler {
        private StatesCollector States { get; }

        public FireCommand FireCommand { get; }
        public MoveCommand MoveCommand { get; }
        public RotateCommand RotateCommand { get; }
        public ContinueCommand ContinueCommand { get; }

        public InputHandler(StatesCollector states) {
            States = states;

            FireCommand = new FireCommand(States.weapon);
            MoveCommand = new MoveCommand(States.entity.player.State);
            RotateCommand = new RotateCommand(States.entity.player.State);
            ContinueCommand = new ContinueCommand(States.game);
        }

    }
}