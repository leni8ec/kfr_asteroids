namespace Core.Framework.Systems {
    public interface ISystem {

        /// <summary>
        /// System active state
        /// </summary>
        public bool Active { get; }

        /// <summary>
        /// Called after all systems constructors is called (when objects are created)
        /// </summary>
        void Initialize();

    }
}