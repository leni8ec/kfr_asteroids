using Model.Core.Interface.Entity;

namespace Control.Model {
    public interface IEntityCreateListener {

        void OnCreate(IEntity entity);

    }
}