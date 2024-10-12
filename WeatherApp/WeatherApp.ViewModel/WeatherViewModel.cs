using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WeatherApp.ViewModel.Annotations;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;
using WeatherAppModel;

namespace WeatherApp.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        
        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        public CurrentConditions CurrentConditions
        {
            get => _currentConditions;
            set
            {
                _currentConditions = value;
                OnPropertyChanged(nameof(CurrentConditions));
            }
        }

        public City SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                if (_selectedCity != null)
                {
                    OnPropertyChanged(nameof(SelectedCity));
                    GetCurrentConditions();    
                }
                
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        private string _query;
        private CurrentConditions _currentConditions;
        private City _selectedCity;

        public event PropertyChangedEventHandler? PropertyChanged;
        
        public SearchCommand SearchCommand { get; set; }

        public WeatherViewModel()
        {
            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }
        
        public async Task MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            Cities.Clear();
            for (int i = 0; i < cities.Count; i++)
            {
                Cities.Add(cities[i]);
            }
        }
        
        public async Task GetCurrentConditions()
        {
            Query = string.Empty;
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
            
            Cities.Clear();
        }
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}