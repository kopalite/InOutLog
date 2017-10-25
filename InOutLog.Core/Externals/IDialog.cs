using System;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public interface IDialog
    {
        void Alert(string title, string message);

        Task OptionAsync(string title, string message, Action yesAction, Action noAction);
    }
}
