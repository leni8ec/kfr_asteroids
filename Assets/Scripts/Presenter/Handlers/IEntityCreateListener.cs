using Model.Entity.Interface;

namespace Presenter.Handlers {
    public interface IEntityCreateListener {

        void OnCreate(IEntity entity);

    }
}