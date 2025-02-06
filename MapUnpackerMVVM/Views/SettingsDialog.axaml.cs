using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MapUnpacker;
using MapUnpackerMVVM.ViewModels;
using System.IO;
using System.Text.Json;
using System;
using Avalonia.VisualTree;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Linq;

namespace MapUnpackerMVVM.Views;

public partial class SettingsDialog : UserControl
{
    private readonly string settingsFilePath = "settings.json";
    private SettingsDialogViewModel _viewModel;

    public SettingsDialog()
    {
        InitializeComponent();
        _viewModel = new SettingsDialogViewModel();
        DataContext = _viewModel;
        LoadSettings(); // Load settings when the dialog opens
    }
    public SettingsDialog(SettingsDialogViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
        LoadSettings();
    }

    private void LoadSettings()
    {
        try
        {
            if (File.Exists(settingsFilePath))
            {
                string json = File.ReadAllText(settingsFilePath);
                var settings = JsonSerializer.Deserialize<SettingsDialogViewModel>(json);

                if (settings != null)
                {
                    _viewModel.SkipMissingGeometry = settings.SkipMissingGeometry;
                    _viewModel.DefaultExportAll = settings.DefaultExportAll;
                    _viewModel.DefaultExportRegMap = settings.DefaultExportRegMap;
                    _viewModel.DefaultExportGeometry = settings.DefaultExportGeometry;
                    _viewModel.DefaultExportJson = settings.DefaultExportJson;
                    _viewModel.DefaultExportObj = settings.DefaultExportObj;
                    _viewModel.DefaultExportPlaintext = settings.DefaultExportPlaintext;
                }
                Global.Print("Loaded Settings from File.");
            }
            else
            {
                // Default values (all true)
                _viewModel.SkipMissingGeometry = true;
                _viewModel.DefaultExportAll = true;
                _viewModel.DefaultExportRegMap = true;
                _viewModel.DefaultExportGeometry = true;
                _viewModel.DefaultExportJson = true;
                _viewModel.DefaultExportObj = true;
                _viewModel.DefaultExportPlaintext = true;

                Global.Print("Initalised default values.");
            }
        }
        catch (Exception ex)
        {
            Global.Print("Error loading settings: " + ex.Message);
        }
    }

    private void OnSaveClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            string json = JsonSerializer.Serialize(_viewModel, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(settingsFilePath, json);

            // Update global settings
            Global.SkipMissingGeometry = _viewModel.SkipMissingGeometry;
            Global.DefaultExportAll = _viewModel.DefaultExportAll;
            Global.DefaultExportRegMap = _viewModel.DefaultExportRegMap;
            Global.DefaultExportGeometry = _viewModel.DefaultExportGeometry;
            Global.DefaultExportJson = _viewModel.DefaultExportJson;
            Global.DefaultExportObj = _viewModel.DefaultExportObj;
            Global.DefaultExportPlaintext = _viewModel.DefaultExportPlaintext;

            Global.Print("Settings saved successfully.");
        }
        catch (Exception ex)
        {
            Global.Print($"Error saving settings: {ex.Message}");
        }
    }

    private void OnCloseClick(object? sender, RoutedEventArgs e)
    {
        this.IsVisible = false; // Hide the dialog
    }

    private async void OnClearFoldersClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            var folderPath = Path.GetFullPath("Resources\\Maps");
            var window = this.GetVisualRoot() as Window;

            if (window != null)
            {
                var messageBox = MessageBoxManager
                    .GetMessageBoxStandard("Confirm Deletion",
                    $"Are you sure you want to delete files in: {folderPath}?",
                    ButtonEnum.YesNo,
                    Icon.Warning);

                var result = await messageBox.ShowWindowDialogAsync(window);
                if (result != ButtonResult.Yes)
                {
                    return; // User canceled deletion
                }
            }

            string[] files = Directory.GetFiles(folderPath, "*.regmap", SearchOption.AllDirectories);
            string[] files2 = Directory.GetFiles(folderPath, "*.cache", SearchOption.AllDirectories);

            foreach (var file in files2.Concat(files))
            {
                File.Delete(file);
            }

            Global.Print("Selected folder cleaned successfully.");
        }
        catch (Exception ex)
        {
            Global.Print($"Error clearing folders: {ex.Message}");
        }
    }

}