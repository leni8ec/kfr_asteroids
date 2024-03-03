using Model.Entity.Interface;
using Presenter.Binders;
using UnityEngine;
using UnityView.Entity.Base;
using Object = UnityEngine.Object;

namespace UnityView.Binders {
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