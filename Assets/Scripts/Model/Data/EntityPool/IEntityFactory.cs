using Model.Entity.Interface;

namespace Model.Data.EntityPool {
    public interface IEntityFactory<out TEntity> where TEntity : IEntity {

        TEntity CreateEntity();

    }
}