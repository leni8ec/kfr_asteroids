using Model.Core.Interface.Entity;

namespace Control.Entity {
    public interface IEntityCreateListener {

        void OnCreate(IEntity entity);

    }
}