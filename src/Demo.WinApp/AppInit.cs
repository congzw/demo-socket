using Common;

namespace Demo.WinApp
{
    public class AppInit
    {
        public static void Init()
        {
            SetupAsyncLog();
        }
        
        private static ISimpleLogFactory SetupAsyncLog()
        {
            var simpleLogFactory = SimpleLogFactory.Resolve();
            simpleLogFactory.LogWithSimpleEventBus();
            return simpleLogFactory;
        }
    }
}
