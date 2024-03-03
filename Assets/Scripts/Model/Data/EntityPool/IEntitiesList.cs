using System;
using System.Collections.Generic;
using Model.Entity.Interface;

namespace Model.Data.EntityPool {
    public interface IEntitiesList<out TEntity> : IReadOnlyCollection<TEntity> where TEntity : IEntity {

        public void ForEachSave(Action<TEntity> action);

    }
}