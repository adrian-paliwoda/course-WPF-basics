using Shared.Extensions;

namespace Contacts;

public partial class ContactView : ContentView
{
    public static readonly BindableProperty NameProperty =
        BindableProperty.Create(nameof(Name), typeof(string), typeof(ContactView));

    public static readonly BindableProperty EmailProperty =
        BindableProperty.Create(nameof(Email), typeof(string), typeof(ContactView));

    public static readonly BindableProperty PhoneProperty =
        BindableProperty.Create(nameof(Phone), typeof(string), typeof(ContactView));

    public static readonly BindableProperty IsEditModeEnabledProperty =
        BindableProperty.Create(nameof(IsEditModeEnabled), typeof(bool), typeof(ContactView),
            propertyChanged:
            (bindable, value, newValue) =>
            {
                var control = (ContactView)bindable;

                control.IsEditModeEnabled = (bool)newValue;
                control.NameEntry.IsEnabled = (bool)newValue;
                control.EmailEntry.IsEnabled = (bool)newValue;
                control.PhoneEntry.IsEnabled = (bool)newValue;
            });
    
    public ContactView()
    {
        InitializeComponent();
        ResetPage();
    }

    public string Name
    {
        get => GetValue(NameProperty) as string ?? string.Empty;
        set => SetValue(NameProperty, value);
    }

    public string Email
    {
        get => GetValue(EmailProperty) as string ?? string.Empty;
        set => SetValue(EmailProperty, value);
    }

    public string Phone
    {
        get => GetValue(PhoneProperty) as string ?? string.Empty;
        set => SetValue(PhoneProperty, value);
    }

    public bool IsEditModeEnabled
    {
        get => (bool)GetValue(IsEditModeEnabledProperty);
        set => SetValue(IsEditModeEnabledProperty, value);
    }

    private void OnTextChanged(object? sender, EventArgs e)
    {
        if (sender is Entry entry)
        {
            if (entry.Text.IsValid())
            {
                entry.SetStylesToValid(Resources);
            }
            else
            {
                entry.SetStylesToError(Resources);
            }
        }
    }
    
    private void ResetPage()
    {
        if (Resources.TryGetValue("EntryEmpty", out var entryEmpty) && entryEmpty is Style entryEmptyStyle)
        {
            NameEntry.Style = entryEmptyStyle;
            EmailEntry.Style = entryEmptyStyle;
            PhoneEntry.Style = entryEmptyStyle;
        }
    }
}