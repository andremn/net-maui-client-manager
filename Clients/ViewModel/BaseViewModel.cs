using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Clients.ViewModel;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void SetValue<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
    {
        if (!EqualityComparer<T>.Default.Equals(property, value))
        {
            property = value;
            NotifyPropertyChanged(propertyName);
        }        
    }

    protected void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}