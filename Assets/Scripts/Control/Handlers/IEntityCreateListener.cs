using Model.Core.Entity.Interface;

namespace Control.Handlers {
    public interface IEntityCreateListener {

        void OnCreate(IEntity entity);

    }
}