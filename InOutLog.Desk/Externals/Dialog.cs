using InOutLog.Core;
using System.Windows;
using System;
using System.Threading.Tasks;

namespace InOutLog.Desk
{
    internal class Dialog : IDialog
    {
        public void Alert(string title, string message)
        {
            MessageBox.Show(message, title);
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
