using Contacts.Database;
using Shared.Extensions;
using Contact = Contacts.Model.Contact;

namespace Contacts;

public partial class DetailContactPage : ContentPage
{
    private readonly ContactsDatabaseAccessLayer _contactsDatabaseAccessLayer;

    public DetailContactPage(ContactsDatabaseAccessLayer contactsDatabaseAccessLayer, Contact contact)
    {
        InitializeComponent();
        BindingContext = contact;
        _contactsDatabaseAccessLayer = contactsDatabaseAccessLayer;
        ResetPage();
    }

    private void ResetPage()
    {
        UpdateButton.IsEnabled = false;
        RemoveButton.IsEnabled = false;
        ContactView.IsEnabled = false;
        
        if (Resources.TryGetValue("ErrorLabel", out var errorLabel) && errorLabel is Style errorLabelStyle)
        {
            ErrorMessage.Style = errorLabelStyle;
        }
    }

    private async void Update_OnClicked(object? sender, EventArgs e)
    {
        ErrorMessage.Text = string.Empty;
        if (BindingContext is not Contact bindingContext)
        {
            return;
        }
        
        var contact = new Contact
        {
            Id = bindingContext.Id,
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
            await _contactsDatabaseAccessLayer.UpdateContact(contact);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }

        await Navigation.PopAsync();
    }

    private async void Delete_OnClicked(object? sender, EventArgs e)
    {
        if (BindingContext is Contact contact)
        {
            try
            {
                await _contactsDatabaseAccessLayer.RemoveContact(contact.Id);
                await Navigation.PopAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }
    }

    private void ModeButton_Click(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            var isEnable = !UpdateButton.IsEnabled;
            button.Text = isEnable ? "Turn Edit Mode OFF" : "Turn Edit Mode ON";
            
            UpdateButton.IsEnabled = isEnable;
            RemoveButton.IsEnabled = isEnable;
            ContactView.IsEnabled = isEnable;
        }
        
    }
}