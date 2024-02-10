namespace Model.Domain.Systems.Base {
    public abstract class SystemBase : ISystem {

        /// <summary>
        /// System active state (enabled by default)
        /// </summary>
        public bool Active { get; private set; } = true;

        protected void Enable() {
            Active = true;
        }

        protected void Disable() {
            Active = false;
        }

    }
}