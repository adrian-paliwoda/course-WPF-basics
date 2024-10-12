using System.Windows;
using System.Windows.Controls;
using Evernote.Model;

namespace EvernoteWpf.View.UserControls
{
    public partial class DisplayNotebookControl : UserControl
    {
        public Notebook Notebook
        {
            get => (Notebook) GetValue(NotebookProperty);
            set => SetValue(NotebookProperty, value);
        }

        public static readonly DependencyProperty NotebookProperty = DependencyProperty.Register(nameof(Notebook),
            typeof(Notebook), typeof(DisplayNotebookControl), new PropertyMetadata(null, SetValues));

        public DisplayNotebookControl()
        {
            InitializeComponent();
        }

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DisplayNotebookControl displayNotebookControl)
            {
                displayNotebookControl.DataContext = displayNotebookControl.Notebook;
            }
        }
    }
}