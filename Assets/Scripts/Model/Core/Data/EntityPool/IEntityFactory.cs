using Model.Core.Entity.Interface;

namespace Model.Core.Data.EntityPool {
    public interface IEntityFactory<out TEntity> where TEntity : IEntity {

        TEntity CreateEntity();

    }
}