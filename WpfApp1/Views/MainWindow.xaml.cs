using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
        private void SwitchToLightTheme()
        {
            var lightTheme = new ResourceDictionary() { Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative) };
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(lightTheme);
        }

        private void SwitchToDarkTheme()
        {
            var darkTheme = new ResourceDictionary() { Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative) };
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(darkTheme);
        }
        private bool IsDarkTheme()
        {
            return Application.Current.Resources.MergedDictionaries
                .Any(d => d.Source.OriginalString.Contains("DarkTheme"));
        }
        private void OnThemeSwitchButtonClick(object sender, RoutedEventArgs e)
        {
            if (IsDarkTheme())
            {
                SwitchToLightTheme();
            }
            else
            {
                SwitchToDarkTheme();
            }
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double windowWidth = this.ActualWidth;

            gridViewColumnName.Width = windowWidth * 0.2;
            gridViewColumnSymbol.Width = windowWidth * 0.1;
            gridViewColumnPrice.Width = windowWidth * 0.3;
            gridViewColumnChange.Width = windowWidth * 0.3;
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCrypto = (Crypto)listview.SelectedItem;
            if (selectedCrypto != null)
            {
                OpenCryptoDetails(selectedCrypto);
            }
        }
        private async void OpenCryptoDetails(Crypto selectedCrypto)
        {
            if (selectedCrypto == null)
            {
                MessageBox.Show("No cryptocurrency selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Console.WriteLine("Crypto ID: " + selectedCrypto.Id);
            var cryptoDetailsPage = App.Services.GetRequiredService<CryptoDetailsPage>();
            var detailsViewModel = App.Services.GetRequiredService<CryptoDetailsViewModel>();
            cryptoDetailsPage.DataContext = detailsViewModel;

            try
            {
                await detailsViewModel.LoadCryptoDetails(selectedCrypto.Id);

                ContentFrame.Content = cryptoDetailsPage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
