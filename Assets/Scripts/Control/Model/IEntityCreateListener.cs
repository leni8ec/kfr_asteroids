using Model.Core.Interface.Objects;

namespace Control.Model {
    public interface IEntityCreateListener {

        void OnCreate(IEntity entity);

    }
}