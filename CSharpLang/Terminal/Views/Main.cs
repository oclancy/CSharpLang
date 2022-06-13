
using System.Collections;

using Terminal.Gui;

namespace Terminal.Views
{
    internal class Main
    {
        public Window View { get; private set; }

        public event EventHandler<ListViewItemEventArgs> SelectedItemChanged;

        ListView List;
        public Main()
        {
            View = new Window("Stuff")
            {
                // By using Dim.Fill(), it will automatically resize without manual intervention
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            List = new ListView();
            List.SelectedItemChanged += List_SelectedItemChanged;
            View.Add(List);
        }

        public void SetOptions(IList options)
        {
            List.SetSource(options);
            List.SelectedItem = 0;
        }

        private void List_SelectedItemChanged(ListViewItemEventArgs obj)
        {
            if (SelectedItemChanged != null)
                SelectedItemChanged(this, obj);
        }
    }
}
