using System;
using System.Collections.Generic;
using Model.Core.Entity.Interface;

namespace Model.Core.Data.EntityPool {
    public class EntityList<TEntity> : List<TEntity>, IEntitiesList<TEntity> where TEntity : IEntity {

        public EntityList(int capacity) : base(capacity) { }

        /// <summary>
        /// Iterate for all active entities.<br/><br/>
        /// It's safe for list changes during iteration.
        /// </summary>
        public void ForEachSave(Action<TEntity> action) {
            for (int i = Count - 1; i >= 0; i--) {
                action(this[i]);
            }
        }

    }
}