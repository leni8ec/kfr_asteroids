using System.Collections.Generic;
using Core.Interface.State;
using Core.Objects.Base;
using UnityEngine;

namespace Core.Pools.Base {
    /// <summary>
    /// Call 'Entity.Destroy()' for Return entity to Pool
    /// </summary>
    public class EntityPool<TEntity, TState, TConfig> where TEntity : Entity<TState, TConfig>
        where TState : class, IStateData, new()
        where TConfig : ScriptableObject, new() {

        private readonly GameObject prefab;
        private readonly TState state;
        private readonly TConfig config;

        private readonly Stack<TEntity> stack = new();
        public readonly List<TEntity> active = new();

        public EntityPool(GameObject prefab, TConfig config) {
            this.prefab = prefab;
            this.config = config;
        }

        private TEntity CreateNewEntity() {
            GameObject go = Object.Instantiate(prefab);
            TEntity entity = go.GetComponent<TEntity>();
            entity.SetData(new TState(), config);
            entity.Dispose += e => Return(entity);
            return entity;
        }


        public TEntity Take() {
            if (!stack.TryPop(out TEntity entity)) {
                entity = CreateNewEntity();
            }

            if (!entity.gameObject.activeSelf)
                entity.gameObject.SetActive(true);

            active.Add(entity);
            return entity;
        }

        private void Return(TEntity entity) {
            entity.gameObject.SetActive(false);
            active.Remove(entity);
            stack.Push(entity);
        }

    }
}