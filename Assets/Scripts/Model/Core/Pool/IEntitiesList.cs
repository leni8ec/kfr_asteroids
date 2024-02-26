using System;
using System.Collections.Generic;
using Model.Core.Interface.Entity;

namespace Model.Core.Pool {
    public interface IEntitiesList<out TEntity> : IReadOnlyCollection<TEntity> where TEntity : IEntity {

        public void ForEachSave(Action<TEntity> action);

    }
}