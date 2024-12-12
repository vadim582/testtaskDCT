using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Crypto> _topCryptos;
        public ObservableCollection<Crypto> TopCryptos
        {
            get { return _topCryptos; }
            set { _topCryptos = value; OnPropertyChanged(); }
        }

        private readonly ICryptoService _cryptoService;

        public MainViewModel(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
            LoadTopCryptos();
        }

        private async void LoadTopCryptos()
        {
            var cryptos = await _cryptoService.GetTopCryptos();
            TopCryptos = new ObservableCollection<Crypto>(cryptos);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
