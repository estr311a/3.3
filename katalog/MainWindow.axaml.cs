using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class MainWindow : Window
{
    private readonly ItemsListView _listView;
    private readonly ItemDetailsView _detailsView;
    private readonly AddItemView _addView;

    public MainWindow()
    {
        InitializeComponent();

        _listView = new ItemsListView();
        _detailsView = new ItemDetailsView();
        _addView = new AddItemView();

        RegisterCallbacks();
        ShowListView();
    }

    private void RegisterCallbacks()
    {
        _listView.ItemSelected = OnItemSelected;
        _listView.AddNewRequested = ShowAddView;

        _detailsView.BackRequested = ShowListView;

        _addView.ItemAdded = OnItemAdded;
        _addView.CancelRequested = ShowListView;
    }

    private void ShowListView()
    {
        MainContent.Content = _listView;
    }

    private void ShowDetailsView(CatalogItem? item)
    {
        if (item != null)
        {
            _detailsView.SetItem(item);
        }
        MainContent.Content = _detailsView;
    }

    private void ShowAddView()
    {
        _addView.ResetForm();
        MainContent.Content = _addView;
    }

    private void OnItemSelected(CatalogItem item)
    {
        ShowDetailsView(item);
    }

    private void OnItemAdded()
    {
        _listView.LoadItems();
        ShowListView();
    }

    private void OnNavListClick(object? sender, RoutedEventArgs e)
    {
        ShowListView();
    }

    private void OnNavDetailsClick(object? sender, RoutedEventArgs e)
    {
        ShowDetailsView(null);
    }

    private void OnNavAddClick(object? sender, RoutedEventArgs e)
    {
        ShowAddView();
    }

    private void OnCountClick(object? sender, RoutedEventArgs e)
    {
        var lines = File.ReadAllLines("dane/pozycje.txt");
        CountText.Text = $"Pozycji w pliku: {lines.Length}";
    }
}
