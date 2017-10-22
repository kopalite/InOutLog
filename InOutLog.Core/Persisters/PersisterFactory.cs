namespace InOutLog.Core
{
    public class PersisterFactory
    {
        public static ILogPersister Create()
        {
            return new RemotePersister(new LocalPersister());
        }
    }
}
