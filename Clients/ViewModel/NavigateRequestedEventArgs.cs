namespace Clients.ViewModel;

public class NavigateRequestedEventArgs(ContentPage targetPage) : EventArgs
{
    public ContentPage TargetPage => targetPage;
}