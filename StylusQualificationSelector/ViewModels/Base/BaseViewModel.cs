using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StylusQualificationSelector
{
    /// <summary>
    /// Base common viewmodel
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event to fire when child property changes value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Method that calls PropertyChanged 
        /// </summary>
        /// <param name="name"> name of the property that changes </param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
