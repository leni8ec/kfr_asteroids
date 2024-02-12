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

        private readonly Stack<TEntity> stack = new();
        private readonly LinkedList<TEntity> active = new(); // use Linked List - as better performance for many add/remove events
        public IEnumerable<TEntity> Active => active;

        /// <summary>
        /// Don't in List mutable cases (don't safe to list changes)
        /// </summary>
        public int ActiveCount => active.Count;

        protected EntityPool(IEntityFactory<TEntity> factory) {
            this.factory = factory;
        }


        public TEntity Take() {
            if (!stack.TryPop(out TEntity entity)) {
                entity = factory.CreateEntity();
                entity.DestroyEvent += () => Return(entity);
            }

            if (!entity.State.Active.Value) entity.State.Active.Value = true; // todo: move from this (or not)?

            active.AddLast(entity);
            return entity;
        }

        /// <summary>
        /// Call 'Entity.Destroy()' to Return entity back to Pool
        /// </summary>
        private void Return(TEntity entity) {
            entity.State.Active.Value = false; // todo: move from this (or not)?
            active.Remove(entity);
            stack.Push(entity);
        }


        /// <summary>
        /// Iterate for all active entities (safe for list changes during iteration)
        /// </summary>
        public void ForEachActive(Action<TEntity> action) {
            LinkedListNode<TEntity> node = active.First;
            while (node != null) {
                LinkedListNode<TEntity> nextNode = node.Next;
                action(node.Value);
                node = nextNode;
            }

        }

    }
}