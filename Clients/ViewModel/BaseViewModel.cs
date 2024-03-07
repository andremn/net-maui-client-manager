using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Clients.ViewModel;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public event NavigateRequestedEventHandler? NavigateToPageRequested;

    public event EventHandler? NavigateBackRequested;

    protected void SetValue<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(property, value)) return;
        
        property = value;
        NotifyPropertyChanged(propertyName);
    }

    private void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void NotifyNavigateToPageRequested(ContentPage targetPage) =>
        NavigateToPageRequested?.Invoke(this, new NavigateRequestedEventArgs(targetPage));

    protected void NotifyNavigateBackRequested() =>
        NavigateBackRequested?.Invoke(this, EventArgs.Empty);
}