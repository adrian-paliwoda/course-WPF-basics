using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using PredictionAiConnector;

namespace LandmarkAI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg; *.png)|*.jpeg;*.jpg;*.png|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (openFileDialog.ShowDialog() != null)
            {
                var fileName = openFileDialog.FileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    SelectedImage.Source = new BitmapImage(new Uri(fileName));

                    var predictions = RequestsManager.MakePredictionAsync(fileName).Result?.ToList();
                    PredictionListView.ItemsSource = predictions;
                }
            }
        }
    }
}