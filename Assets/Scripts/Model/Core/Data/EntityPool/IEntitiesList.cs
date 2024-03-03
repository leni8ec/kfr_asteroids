using System;
using System.Collections.Generic;
using Model.Core.Entity.Interface;

namespace Model.Core.Data.EntityPool {
    public interface IEntitiesList<out TEntity> : IReadOnlyCollection<TEntity> where TEntity : IEntity {

        public void ForEachSave(Action<TEntity> action);

    }
}