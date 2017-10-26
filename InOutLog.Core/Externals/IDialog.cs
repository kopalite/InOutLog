using System;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public interface IDialog
    {
        Task AlertAsync(string title, string message);

        Task OptionAsync(string title, string message, Action yesAction, Action noAction);
    }
}
