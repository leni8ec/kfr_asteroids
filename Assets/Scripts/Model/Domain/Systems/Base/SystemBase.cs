namespace Model.Domain.Systems.Base {
    public abstract class SystemBase : ISystem {

        public bool Active { get; private set; }

        private bool isStarted;


        protected void Enable() {
            if (Active) return;

            if (!isStarted) {
                isStarted = true;
                if (this is IStartSystem startSystem) startSystem.Start();
            }

            OnEnable();
            Active = true;
        }

        protected void Disable() {
            if (!Active) return;

            OnDisable();
            Active = false;
        }

        public void Initialize() {
            if (this is ICreateSystem createSystem) createSystem.Create();

            // Hack: enable all systems by default
            Enable();
        }


        /// <summary>
        /// Called on system enable call
        /// </summary>
        protected virtual void OnEnable() { }

        /// <summary>
        /// Called on system disable call
        /// </summary>
        protected virtual void OnDisable() { }

    }
}