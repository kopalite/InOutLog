using InOutLog.Core;
using System.Windows;

namespace InOutLog.Desk
{
    internal class Dialog : IDialog
    {
        public void Alert(string title, string message)
        {
            MessageBox.Show(message, title);
        }

        public bool Option(string title, string message)
        {
            var answer = MessageBox.Show(message, title, MessageBoxButton.YesNo);
            return answer == MessageBoxResult.Yes;
        }
    }
}
