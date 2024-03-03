using Model.Entity.Interface;

namespace Presenter.Binders {
    public interface IEntityCreateListener {

        void OnCreate(IEntity entity);

    }
}