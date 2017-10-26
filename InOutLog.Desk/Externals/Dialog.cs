using InOutLog.Core;
using System.Windows;
using System;
using System.Threading.Tasks;

namespace InOutLog.Desk
{
    internal class Dialog : IDialog
    {
        public async Task AlertAsync(string title, string message)
        {
            MessageBox.Show(message, title);
            await Task.FromResult<object>(null);
        }

        public async Task OptionAsync(string title, string message, Action yesAction, Action noAction)
        {
            var answer = MessageBox.Show(message, title, MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {
                yesAction();
            }
            else
            {
                noAction();
            }

            await Task.FromResult<object>(null);
        }
    }
}
