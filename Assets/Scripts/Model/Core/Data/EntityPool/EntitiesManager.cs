using System.Collections.Generic;
using Model.Core.Data.State.Base;
using Model.Core.Data.Unity.Config.Base;
using Model.Core.Entity.Base;

namespace Model.Core.Data.EntityPool {
    // todo: Find better naming?
    public class EntitiesManager<TEntity, TState, TConfig>
        where TEntity : Entity<TState, TConfig>, new()
        where TState : EntityState, new()
        where TConfig : IConfigData {

        /// <summary>
        /// Entities pool
        /// </summary>
        private readonly Stack<TEntity> pool;
        /// <summary>
        /// Active entities
        /// </summary>
        private readonly EntityList<TEntity> active;
        /// <summary>
        /// Factory method to create new entities for pool
        /// </summary>
        private readonly EntityFactory<TEntity, TState, TConfig> factory;


        public EntitiesManager(TConfig config, int capacity, out IEntitiesList<TEntity> activeEntities) {
            factory = new EntityFactory<TEntity, TState, TConfig>(config);

            pool = new Stack<TEntity>(capacity);
            active = new EntityList<TEntity>(capacity);
            activeEntities = active;
        }


        public TEntity TakeEntity() {
            TEntity entity;
            if (pool.Count == 0) {
                entity = factory.CreateEntity();
                entity.DestroyEvent += () => Return(entity);
            } else {
                entity = pool.Pop();
            }

            entity.State.Active.Value = true;
            active.Add(entity);
            return entity;
        }

        /// <summary>
        /// Call 'Entity.Destroy()' to Return entity back to Pool
        /// </summary>
        private void Return(TEntity entity) {
            entity.State.Active.Value = false;
            active.Remove(entity); // Complexity: O(N)
            pool.Push(entity);
        }

    }
}