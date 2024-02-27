namespace Model.Domain.Systems.Base {
    public interface IStartSystem : ISystem {

        /// <summary>
        /// Called only once when the system is first enabled<br/><br/>
        /// Called after `Create()`
        /// </summary>
        void Start();

    }
}