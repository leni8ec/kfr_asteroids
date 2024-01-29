namespace Framework.Objects {
    public interface IPlayer : IEntity {

        void Fire();
        void Rotate(bool left);
        void Move();

    }
}