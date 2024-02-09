using Control.Model;
using Model.Core.Interface.Entity;
using Model.Core.Interface.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityView.Handlers {
    public class UnityEntityCreateListener<TEntityView> : IEntityCreateListener where TEntityView : IEntityView {

        protected GameObject prefab;

        public UnityEntityCreateListener(GameObject prefab) {
            this.prefab = prefab;
        }

        public virtual void OnCreate(IEntity entity) {
            GameObject playerObject = Object.Instantiate(prefab);
            TEntityView entityView = playerObject.GetComponent<TEntityView>();
            entityView.Create(entity);
        }

    }
}