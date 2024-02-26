using Model.Core.Data.State.Base;
using Model.Core.Entity.Base;
using Model.Core.Interface.Config;

namespace Model.Core.Pool {
    public class EntityFactory<TEntity, TState, TConfig> : IEntityFactory<TEntity>
        where TEntity : Entity<TState, TConfig>, new()
        where TState : EntityState, new()
        where TConfig : IConfigData {

        private readonly TConfig config;

        public EntityFactory(TConfig config) {
            this.config = config;
        }

        public TEntity CreateEntity() {
            TEntity entity = new();
            entity.Create(config);
            return entity;
        }
    }
}