using Common;

namespace Demo.WinApp.UI
{
    public class LazySimpleLog<T>
    {
        public void LogInfo(string message)
        {
            var simpleLogFactory = SimpleLogFactory.Resolve();
            var simpleLog = simpleLogFactory.GetOrCreateLogFor<T>();
            simpleLog.LogInfo(message);
        }

        public static LazySimpleLog<T> Instance = new LazySimpleLog<T>();
    }
}