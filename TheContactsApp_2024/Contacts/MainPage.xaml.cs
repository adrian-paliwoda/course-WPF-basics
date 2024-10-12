using System.Windows.Input;
using Contacts.Database;
using Contact = Contacts.Model.Contact;

namespace Contacts;

public partial class MainPage : ContentPage
{
    private readonly ContactsDatabaseAccessLayer _contactsDatabaseAccessLayer;
    public List<Contact> _data;
    public IEnumerable<Contact> _filterddata;

    public MainPage()
    {
        InitializeComponent();
        _contactsDatabaseAccessLayer = new ContactsDatabaseAccessLayer();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ReadDataFromDb();
    }

    private async Task ReadDataFromDb()
    {
        _data = (await _contactsDatabaseAccessLayer
                .GetTable())
            .OrderBy(p => p.Id)
            .ToList();

        ContactsView.ItemsSource = _data;
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        var page = new NewContactPage(_contactsDatabaseAccessLayer);

        await Navigation.PushAsync(page);
    }

    private void SearchBar_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is SearchBar searchBar
            && !string.IsNullOrEmpty(searchBar.Text)
            && !string.IsNullOrWhiteSpace(searchBar.Text))
        {
            var searchText = searchBar.Text;
            _filterddata = _data.Where(i =>
                    i.Name.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)
                    || i.Email.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)
                    || i.Phone.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)
                ).OrderBy(p => p.Id)
                ;

            ContactsView.ItemsSource = _filterddata;
        }
        else
        {
            ContactsView.ItemsSource = _data;
        }
    }

    private async void RemoveContact(object? sender, EventArgs e)
    {
        if (sender is Button { CommandParameter: int contactId })
        {
            await _contactsDatabaseAccessLayer.RemoveContact(contactId)!;
            await ReadDataFromDb();
        }
    }

    private async void Item_OnClicked(object? sender, EventArgs e)
    {
        if (sender is Button { Parent: StackLayout { BindingContext: Contact contact } })
        {
            var page = new DetailContactPage(_contactsDatabaseAccessLayer, contact);

            await Navigation.PushAsync(page);
        }
    }
}