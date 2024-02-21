using System;
using System.Collections.Generic;
using Model.Core.Data.State.Base;
using Model.Core.Entity.Base;
using Model.Core.Interface.Config;

namespace Model.Core.Pools.Base {
    /// <summary>
    ///     Call 'Entity.Destroy()' to Return entity back to Pool
    /// </summary>
    public class EntityPool<TEntity, TState, TConfig>
        where TEntity : Entity<TState, TConfig>, new()
        where TState : EntityState, new()
        where TConfig : IConfigData {

        private readonly IEntityFactory<TEntity> factory;

        private readonly Stack<TEntity> stack;
        // todo: move from this?
        private readonly List<TEntity> active;
        public IList<TEntity> Active => active;

        /// <summary>
        /// Don't in List mutable cases (don't safe to list changes)
        /// </summary>
        public int ActiveCount => active.Count;

        protected EntityPool(IEntityFactory<TEntity> factory, int capacity) {
            this.factory = factory;
            stack = new Stack<TEntity>(capacity);
            active = new List<TEntity>(capacity);
        }


        public TEntity Take() {
            if (!stack.TryPop(out TEntity entity)) {
                entity = factory.CreateEntity();
                entity.DestroyEvent += () => Return(entity);
            }

            if (!entity.State.Active.Value) entity.State.Active.Value = true; // todo: move from this (or not)?

            active.Add(entity);
            return entity;
        }

        /// <summary>
        /// Call 'Entity.Destroy()' to Return entity back to Pool
        /// </summary>
        private void Return(TEntity entity) {
            entity.State.Active.Value = false; // todo: move from this (or not)?
            active.Remove(entity); // Complexity: O(N)
            stack.Push(entity);
        }


        /// <summary>
        /// Iterate for all active entities (safe for list changes during iteration)
        /// </summary>
        public void ForEachActive(Action<TEntity> action) {
            for (int i = active.Count - 1; i >= 0; i--) {
                action(active[i]);
            }
        }

    }
}