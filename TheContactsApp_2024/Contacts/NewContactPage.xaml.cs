using System.Buffers;
using Contacts.Database;
using Shared.Extensions;
using Contact = Contacts.Model.Contact;

namespace Contacts;

public partial class NewContactPage : ContentPage
{
    private readonly ContactsDatabaseAccessLayer _contactsDatabaseAccessLayer;

    public NewContactPage(ContactsDatabaseAccessLayer contactsDatabaseAccessLayer)
    {
        InitializeComponent();
        _contactsDatabaseAccessLayer = contactsDatabaseAccessLayer;
        
        BindingContext = new Contact();
        ResetPage();
    }
    
    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        ErrorMessage.Text = string.Empty;
        if (BindingContext is not Contact bindingContext)
        {
            return;
        }

        var contact = new Contact
        {
            Name = bindingContext.Name,
            Email = bindingContext.Email,
            Phone = bindingContext.Phone
        };

        if (StringCommon.IsNullOrEmptyOrWhiteSpace(new[] { contact.Email, contact.Phone, contact.Phone }))
        {
            ErrorMessage.Text = "Enter valid data!";
            return;
        }

        try
        {
            await _contactsDatabaseAccessLayer.AddContact(contact);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }

        await Navigation.PopAsync();
    }

    private void ResetPage()
    {
        if (Resources.TryGetValue("ErrorLabel", out var errorLabel) && errorLabel is Style errorLabelStyle)
        {
            ErrorMessage.Style = errorLabelStyle;
        }
    }
}