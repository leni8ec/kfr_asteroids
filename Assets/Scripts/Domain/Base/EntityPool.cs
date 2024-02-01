using System.Collections.Generic;
using Presentation.Objects;
using UnityEngine;

namespace Domain.Base {
    public class EntityPool<TEntity, TData> where TEntity : Entity<TData> where TData : ScriptableObject, new() {
        private readonly GameObject prefab;
        private readonly TData data;

        private readonly Stack<TEntity> stack = new();
        public readonly List<TEntity> active = new();

        public EntityPool(GameObject prefab, TData data) {
            this.prefab = prefab;
            this.data = data;
        }


        public TEntity Take() {
            if (!stack.TryPop(out TEntity entity)) {
                // Instantiate new object
                GameObject go = Object.Instantiate(prefab);
                entity = go.GetComponent<TEntity>();
                entity.SetData(data);
                entity.Dispose += e => Return(entity);
                // stack.Push(entity);
            }

            if (!entity.gameObject.activeSelf) entity.gameObject.SetActive(true);
            active.Add(entity);
            return entity;
        }

        public void Return(TEntity entity) {
            entity.gameObject.SetActive(false);
            active.Remove(entity);
            stack.Push(entity);
        }

    }
}