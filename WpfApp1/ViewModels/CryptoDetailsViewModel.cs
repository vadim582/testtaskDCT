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

namespace WpfApp1.ViewModels
{
    public class CryptoDetailsViewModel : INotifyPropertyChanged
    {
        private readonly ICryptoService _cryptoService;
        private Crypto _cryptoDetails;

        public Crypto CryptoDetails
        {
            get { return _cryptoDetails; }
            set
            {
                _cryptoDetails = value;
                OnPropertyChanged();
            }
        }

        public CryptoDetailsViewModel(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
        }

        public async Task LoadCryptoDetails(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Invalid crypto ID", nameof(id));
            CryptoDetails = await _cryptoService.GetCryptoDetails(id);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}