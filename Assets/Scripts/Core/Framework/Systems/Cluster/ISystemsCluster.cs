namespace Core.Framework.Systems.Cluster {
    public interface ISystemsCluster {

        public void Add<T>(T system) where T : ISystem;

        /// <summary>
        /// Called after all systems constructors is called
        /// </summary>
        public void Initialization();

        void Upd(float deltaTime);
        
    }
}