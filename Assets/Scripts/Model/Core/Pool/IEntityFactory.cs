using Model.Core.Interface.Entity;

namespace Model.Core.Pool {
    public interface IEntityFactory<out TEntity> where TEntity : IEntity {

        TEntity CreateEntity();

    }
}