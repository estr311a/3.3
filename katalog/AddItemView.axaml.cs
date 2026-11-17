using System;
using System.IO;
using System.Text;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class AddItemView : UserControl
{
    public Action? ItemAdded { get; set; }
    public Action? CancelRequested { get; set; }

    public AddItemView()
    {
        InitializeComponent();
    }

    public void ResetForm()
    {
        TitleTextBox.Text = string.Empty;
        YearTextBox.Text = string.Empty;
        RatingTextBox.Text = string.Empty;
        StatusTextBlock.Text = string.Empty;
    }

    private void OnSaveClick(object? sender, RoutedEventArgs e)
    {
        string title = TitleTextBox.Text?.Trim() ?? string.Empty;
        string year = YearTextBox.Text?.Trim() ?? string.Empty;
        string rating = RatingTextBox.Text?.Trim() ?? string.Empty;

        if (!ValidateInputs(title, year, rating))
        {
            return;
        }

        SaveItemToFile(title, year, rating);
        ResetForm();
        ItemAdded?.Invoke();
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        ResetForm();
        CancelRequested?.Invoke();
    }

    private bool ValidateInputs(string title, string year, string rating)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            StatusTextBlock.Text = "Proszę podać tytuł pozycji.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(year))
        {
            StatusTextBlock.Text = "Proszę podać rok.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(rating))
        {
            StatusTextBlock.Text = "Proszę podać ocenę.";
            return false;
        }

        return true;
    }

    private void SaveItemToFile(string title, string year, string rating)
    {
        string line = $"{title};{year};{rating}";
        AppendToFile(GetFilePath(), line);
        AppendToAlternativeFile(line);
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

    private void AppendToFile(string path, string line)
    {
        string? directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.AppendAllLines(path, new[] { line }, Encoding.UTF8);
    }

    private void AppendToAlternativeFile(string line)
    {
        string current = GetFilePath();
        string alternative = current == "dane/pozycje.txt" ? "katalog/dane/pozycje.txt" : "dane/pozycje.txt";
        if (File.Exists(alternative))
        {
            File.AppendAllLines(alternative, new[] { line }, Encoding.UTF8);
        }
    }
}
