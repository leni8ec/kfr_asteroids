namespace Control.Collectors.Base {

    /// <summary>
    /// Used in 'View' layer
    /// </summary>
    /// <typeparam name="TCollector">Concrete collector that must be created</typeparam>
    public interface ICollectorCreator<out TCollector> where TCollector : ICollector {

        TCollector CreateCollector();

    }
}