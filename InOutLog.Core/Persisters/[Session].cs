using System;

namespace InOutLog.Core
{
    public class Session
    {
        public static readonly string SessionId = Guid.NewGuid().ToString("D");
    }
}
