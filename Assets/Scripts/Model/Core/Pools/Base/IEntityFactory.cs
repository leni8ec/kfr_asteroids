using Model.Core.Interface.Entity;

namespace Model.Core.Pools.Base {
    public interface IEntityFactory<out TEntity> where TEntity : IEntity {

        TEntity CreateEntity();

    }
}