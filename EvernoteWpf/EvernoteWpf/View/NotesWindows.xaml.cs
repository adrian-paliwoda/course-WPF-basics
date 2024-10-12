using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using Evernote.Db;
using Evernote.ViewModel;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace EvernoteWpf.View
{
    public partial class NotesWindows : Window
    {
        private readonly NotesViewModel _viewModel;

        public NotesWindows()
        {
            InitializeComponent();

            _viewModel = Resources["Vm"] as NotesViewModel;
            _viewModel.SelectedNoteChanged += ViewModel_OnSelectedNote;

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FontFamilyComboBox.ItemsSource = fontFamilies;

            var fontSizes = new List<double> {8, 9, 10, 11, 12, 14, 25, 26};
            FontSizeComboBox.ItemsSource = fontSizes;
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            const string region = "southcentralus";
            const string key = "87464598e855488dbe3f5c85b71005f2";

            var speechConfig = SpeechConfig.FromSubscription(key, region);
            using (var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
            {
                using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
                {
                    var result = await recognizer.RecognizeOnceAsync();
                    ContentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(result.Text)));
                }
            }
        }

        private void ContentRichTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var amountOfCharacters = new TextRange(ContentRichTextBox.Document.ContentStart,
                ContentRichTextBox.Document.ContentEnd).Text.Length;

            StatusTextBlock.Text = string.Concat("Text length ", amountOfCharacters);
        }

        private void BoldButton_OnClick(object sender, RoutedEventArgs e)
        {
            var isChecked = ((ToggleButton) sender).IsChecked ?? false;
            ContentRichTextBox?.Selection.ApplyPropertyValue(TextElement.FontWeightProperty,
                isChecked ? FontWeights.Bold : FontWeights.Normal);
        }

        private void ContentRichTextBox_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedWeight = ContentRichTextBox.Selection.GetPropertyValue(FontWeightProperty);
            BoldButton.IsChecked = selectedWeight != DependencyProperty.UnsetValue &&
                                   selectedWeight.Equals(FontWeights.Bold);

            var selectedStyle = ContentRichTextBox.Selection.GetPropertyValue(FontStyleProperty);
            ItalicButton.IsChecked = selectedStyle != DependencyProperty.UnsetValue &&
                                     selectedStyle.Equals(FontStyles.Italic);

            var selectedDecoration = ContentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            UnderLineButton.IsChecked = selectedDecoration != DependencyProperty.UnsetValue &&
                                        selectedDecoration.Equals(TextDecorations.Underline);

            FontFamilyComboBox.SelectedItem =
                ContentRichTextBox.Selection.GetPropertyValue(TextElement.FontFamilyProperty);

            if (ContentRichTextBox.Selection.GetPropertyValue(TextElement.FontSizeProperty) ==
                DependencyProperty.UnsetValue)
            {
                FontSizeComboBox.Text = string.Empty;
            }
            else
            {
                FontSizeComboBox.Text = ContentRichTextBox.Selection.GetPropertyValue(TextElement.FontSizeProperty)
                                                          .ToString();
            }
        }

        private void ItalicButton_OnClick(object sender, RoutedEventArgs e)
        {
            var isChecked = ((ToggleButton) sender).IsChecked ?? false;
            ContentRichTextBox?.Selection.ApplyPropertyValue(TextElement.FontStyleProperty,
                isChecked ? FontStyles.Italic : FontStyles.Normal);
        }

        private void UnderLineButton_OnClick(object sender, RoutedEventArgs e)
        {
            var isChecked = ((ToggleButton) sender).IsChecked ?? false;
            ContentRichTextBox?.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty,
                isChecked ? TextDecorations.Underline : TextDecorations.Baseline);
        }

        private void FontFamilyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyComboBox.SelectedItem != null)
            {
                ContentRichTextBox.Selection.ApplyPropertyValue(FontFamilyProperty, FontFamilyComboBox.SelectedItem);
            }
        }

        private void FontSizeComboBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (FontSizeComboBox != null && !string.IsNullOrEmpty(FontSizeComboBox.Text))
            {
                ContentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, FontSizeComboBox.Text);
            }
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var fileName =
                $"{_viewModel.SelectedNotebook.Id}_{_viewModel.SelectedNote.Id}_{_viewModel.SelectedNote.Title}.rtf";

            var rtfFilePath = Path.Combine(Environment.CurrentDirectory, fileName);
            _viewModel.SelectedNote.FileLocation = rtfFilePath;

            var contents = new TextRange(ContentRichTextBox.Document.ContentStart,
                ContentRichTextBox.Document.ContentEnd);
            if (!string.IsNullOrEmpty(rtfFilePath))
            {
                using (var fileStream = new FileStream(rtfFilePath, FileMode.Create))
                {
                    contents.Save(fileStream, DataFormats.Rtf);
                }    
            }
            
            //_viewModel.SelectedNote.FileLocation = UpdateFile(rtfFilePath, fileName).Result; // No work -> need to create new azure account
            var result = FirebaseCloudDb.Update(_viewModel.SelectedNote).Result;
            // DatabaseManager.Update(_viewModel.SelectedNote);
        }

        private async Task<string> UpdateFile(string filePath, string fileName)
        {
            return await AzureStorageManager.Update(filePath,fileName);
        }

        private void ViewModel_OnSelectedNote(object? sender, EventArgs e)
        {
            ContentRichTextBox.Document.Blocks.Clear();

            if (_viewModel.SelectedNote != null && !string.IsNullOrEmpty(_viewModel.SelectedNote.FileLocation))
            {
                var contents = new TextRange(ContentRichTextBox.Document.ContentStart,
                    ContentRichTextBox.Document.ContentEnd);
                var downloadPath = $"{_viewModel.SelectedNotebook.Id}_{_viewModel.SelectedNote.Id}_{_viewModel.SelectedNote.Title}.rtf";

                //var file = AzureStorageManager.Download(_viewModel.SelectedNote.FileLocation,downloadPath).Result;
                
                //using (var stream = new FileStream(downloadPath, FileMode.Open))
                using (var stream = new FileStream(Path.Combine(Environment.CurrentDirectory, downloadPath), FileMode.Open))
                {
                    contents.Load(stream, DataFormats.Rtf);
                }
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (string.IsNullOrEmpty(CurrentUser.UserId))
            {
                var loginWindows = new LoginWindow();
                loginWindows.ShowDialog();

                _viewModel.GetNotebooks();
            }
        }
    }
}