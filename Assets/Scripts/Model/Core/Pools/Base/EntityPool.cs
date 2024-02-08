using System;
using System.Collections.Generic;
using Model.Core.Data.State.Base;
using Model.Core.Interface.View;
using Model.Core.Objects.Game.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Model.Core.Pools.Base {
    /// <summary>
    ///     Call 'Entity.Destroy()' for Return entity to Pool
    /// </summary>
    public class EntityPool<TEntity, TState, TConfig> where TEntity : Entity<TState, TConfig>
        where TState : EntityState, new()
        where TConfig : ScriptableObject, new() {

        private readonly Stack<TEntity> stack = new();
        private readonly LinkedList<TEntity> active = new(); // use Linked List - as better performance for many add/remove events
        public IEnumerable<TEntity> Active => active;

        /// <summary>
        /// Don't in List mutable cases (don't safe to list changes)
        /// </summary>
        public int ActiveCount => active.Count;

        private readonly TState state;
        private readonly TConfig config;
        private readonly GameObject prefab;


        protected EntityPool(GameObject prefab, TConfig config) {
            this.prefab = prefab;
            this.config = config;
        }

        private TEntity CreateNewEntity() {
            GameObject entityObject = Object.Instantiate(prefab);
            TEntity entity = (TEntity)entityObject.GetComponent<IEntityView>().EntityLink;
            entity.SetConfig(config);
            entity.Dispose += e => Return(entity);
            return entity;
        }


        public TEntity Take() {
            if (!stack.TryPop(out TEntity entity)) entity = CreateNewEntity();

            if (!entity.GameObject.activeSelf)
                entity.GameObject.SetActive(true);

            active.AddLast(entity);
            return entity;
        }

        private void Return(TEntity entity) {
            entity.GameObject.SetActive(false);
            active.Remove(entity);
            stack.Push(entity);
        }


        /// <summary>
        /// Iterate for all active entities (safe to list changes)
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