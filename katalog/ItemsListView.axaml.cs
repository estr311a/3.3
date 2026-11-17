using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class ItemsListView : UserControl
{
    public Action<CatalogItem>? ItemSelected { get; set; }
    public Action? AddNewRequested { get; set; }

    public ItemsListView()
    {
        InitializeComponent();
        LoadItems();
    }

    public void LoadItems()
    {
        string path = GetFilePath();
        if (!File.Exists(path))
        {
            return;
        }

        var lines = File.ReadAllLines(path, Encoding.UTF8);
        var items = ParseLines(lines);
        ItemsListBox.ItemsSource = items;
    }

    private string GetFilePath()
    {
        if (File.Exists("dane/pozycje.txt"))
        {
            return "dane/pozycje.txt";
        }
        if (File.Exists("katalog/dane/pozycje.txt"))
        {
            return "katalog/dane/pozycje.txt";
        }
        return "dane/pozycje.txt";
    }

    private List<CatalogItem> ParseLines(string[] lines)
    {
        var items = new List<CatalogItem>();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var item = ParseLine(line);
            if (item != null)
            {
                items.Add(item);
            }
        }
        return items;
    }

    private CatalogItem? ParseLine(string line)
    {
        var parts = line.Split(';');
        if (parts.Length < 3)
        {
            return null;
        }

        return new CatalogItem(parts[0].Trim(), parts[1].Trim(), parts[2].Trim());
    }

    private void OnShowDetailsClick(object? sender, RoutedEventArgs e)
    {
        if (ItemsListBox.SelectedItem is CatalogItem selected)
        {
            ItemSelected?.Invoke(selected);
        }
    }

    private void OnAddNewClick(object? sender, RoutedEventArgs e)
    {
        AddNewRequested?.Invoke();
    }

    private void OnRefreshClick(object? sender, RoutedEventArgs e)
    {
        LoadItems();
    }
}
