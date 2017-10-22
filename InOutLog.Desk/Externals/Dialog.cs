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
    }
}
