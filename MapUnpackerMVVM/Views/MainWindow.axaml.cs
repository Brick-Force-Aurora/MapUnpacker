using Avalonia.Controls;
using MapUnpacker;
using System.IO;
using System;
using System.Collections.ObjectModel;
using MapUnpackerMVVM.ViewModels;
using Avalonia.Interactivity;
using Newtonsoft.Json;
using System.Collections.Generic;
using Avalonia.Platform.Storage;

namespace MapUnpackerMVVM.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        private readonly ExportDialogViewModel _exportDialogViewModel;
        
        private readonly SettingsDialogViewModel _settingsDialogViewModel;

        public MainWindow()
        {
            InitializeComponent();
            Global.MainWindowInstance = this;
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
            _exportDialogViewModel = new ExportDialogViewModel();
            _settingsDialogViewModel = new SettingsDialogViewModel();
            if (File.Exists(".\\Assets\\bricks.json"))
            {
                string json = File.ReadAllText(".\\Assets\\bricks.json");
                RegMapManager.bricks = JsonConvert.DeserializeObject<List<Brick>>(json);
                Converter.BuildAliasToRE();
            }
            else
            {
                Global.Print("Could not load bricks.json: " + Path.GetFullPath(".\\Assets\\bricks.json"));
            }
        }

        private async void OnLoadFileClick(object sender, RoutedEventArgs e)
        {
            var files = await this.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select a RegMap file",
                AllowMultiple = false,
                FileTypeFilter = new[] { new FilePickerFileType("RegMap Files") { Patterns = new[] { "*.regmap" } } }
            });

            if (files.Count > 0)
            {
                var filePath = files[0].Path.LocalPath;
                LoadRegMap(filePath);
            }
        }

        private async void OnLoadFolderClick(object sender, RoutedEventArgs e)
        {
            var folders = await this.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = "Select Folder Containing RegMap Files",
                AllowMultiple = false
            });

            if (folders.Count > 0)
            {
                var folderPath = folders[0].Path.LocalPath;
                var files = Directory.GetFiles(folderPath, "*.regmap");
                foreach (var file in files)
                {
                    LoadRegMap(file);
                }
            }
        }

        private async void OnRegMapSelected(object? sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is RegMap selectedRegMap)
            {
                viewModel.SelectedRegMap = selectedRegMap;
            }
        }

        private void LoadRegMap(string filePath)
        {
            var regMap = RegMapManager.Load(filePath); // Assuming RegMapManager.Load returns a RegMap object
            if (regMap != null && regMap.map != -1)
            {
                var viewModel = DataContext as MainWindowViewModel;
                viewModel?.RegMaps.Add(regMap);  // Add to ObservableCollection
                _exportDialogViewModel.RegMaps.Add(regMap);
                Global.Print($"Loaded: {regMap.alias}");
            }
            else
            {
                Global.Print($"Failed to load: {Path.GetFileName(filePath)}");
            }
        }

        private void OnExportAllClick(object? sender, RoutedEventArgs e)
        {
            var exportDialog = new ExportDialog(_exportDialogViewModel)
            {
                IsVisible = true
            };

            Global.ExportDialogInstance = exportDialog;

            // Set Grid position and span
            Grid.SetRow(exportDialog, 0);           // Start at row 0
            Grid.SetColumn(exportDialog, 0);        // Start at column 0
            Grid.SetRowSpan(exportDialog, 3);       // Span across 3 rows
            Grid.SetColumnSpan(exportDialog, 3);    // Span across 3 columns

            // Add to the Grid (assuming 'this.Content' is a Grid)
            if (this.Content is Grid mainGrid)
            {
                mainGrid.Children.Add(exportDialog);
            }
        }

        private void OnSettingsClick(object? sender, RoutedEventArgs e)
        {
            var settingsDialog = new SettingsDialog(_settingsDialogViewModel)
            {
                IsVisible = true
            };

            // Set Grid position and span
            Grid.SetRow(settingsDialog, 0);           // Start at row 0
            Grid.SetColumn(settingsDialog, 0);        // Start at column 0
            Grid.SetRowSpan(settingsDialog, 3);       // Span across 3 rows
            Grid.SetColumnSpan(settingsDialog, 3);    // Span across 3 columns

            // Add to the Grid (assuming 'this.Content' is a Grid)
            if (this.Content is Grid mainGrid)
            {
                mainGrid.Children.Add(settingsDialog);
            }
        }
    }
}