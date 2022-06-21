using Terminal.Gui;

namespace Terminal.Views
{
    internal interface IView
    {
        string Id { get; }
        View View { get; }
    }
}