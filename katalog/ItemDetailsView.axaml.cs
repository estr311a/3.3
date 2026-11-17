using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class ItemDetailsView : UserControl
{
    public Action? BackRequested { get; set; }

    public ItemDetailsView()
    {
        InitializeComponent();
    }

    public void SetItem(CatalogItem item)
    {
        TitleTextBlock.Text = item.Title;
        YearTextBlock.Text = item.Year;
        RatingTextBlock.Text = $"{item.Rating} / 10";
    }

    public void ClearView()
    {
        TitleTextBlock.Text = "(Brak wyboru)";
        YearTextBlock.Text = "-";
        RatingTextBlock.Text = "- / 10";
    }

    private void OnBackClick(object? sender, RoutedEventArgs e)
    {
        BackRequested?.Invoke();
    }
}
