namespace Core.Interface.Objects {
    public interface IPlayer : IEntity {

        void Rotate(bool actionFlag, bool left);

        void Move(bool actionFlag);

    }
}