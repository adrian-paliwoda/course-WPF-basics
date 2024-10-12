using System.Windows;
using System.Windows.Controls;
using Evernote.Model;

namespace EvernoteWpf.View.UserControls
{
    public partial class DisplayNoteControl : UserControl
    {
        public Note Note
        {
            get => (Note) GetValue(NoteProperty);

            set => SetValue(NoteProperty, value);
        }

        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register(nameof(Note), typeof(Note), typeof(DisplayNoteControl),
                new PropertyMetadata(null, SetValue));

        public DisplayNoteControl()
        {
            InitializeComponent();
        }

        private static void SetValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DisplayNoteControl displayNoteControl)
            {
                displayNoteControl.DataContext = displayNoteControl.Note;
            }
        }
    }
}