using Domain.Data;
using Framework.Objects;

namespace Domain.Objects {
    public class Player : Entity<PlayerData>, IPlayer {

        public Player(PlayerData data) : base(data) { }

        public void Fire() { }

        public void Rotate(bool left) { }

        // Move must have inertia and the screen - is infinity

        public void Move() { }

        public void Upd(float deltaTime) { }

    }
}