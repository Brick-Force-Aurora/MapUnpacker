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
using Avalonia;

namespace MapUnpackerMVVM.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            Global.MainWindowInstance = this;
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
            if (File.Exists(".\\Assets\\bricks.json"))
            {
                string json = File.ReadAllText(".\\Assets\\bricks.json");
                RegMapManager.bricks = JsonConvert.DeserializeObject<List<Brick>>(json);
                Converter.BuildAliasToRE();
            }
            else
            {
                Global.PrintLine("Could not load bricks.json: " + Path.GetFullPath(".\\Assets\\bricks.json"));
            }
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(Global.settingsFilePath))
                {
                    string json = File.ReadAllText(Global.settingsFilePath);
                    var settings = System.Text.Json.JsonSerializer.Deserialize<SettingsDialogViewModel>(json);

                    if (settings != null)
                    {
                        Global.SkipMissingGeometry = settings.SkipMissingGeometry;
                        Global.DefaultExportAll = settings.DefaultExportAll;
                        Global.DefaultExportRegMap = settings.DefaultExportRegMap;
                        Global.DefaultExportGeometry = settings.DefaultExportGeometry;
                        Global.DefaultExportJson = settings.DefaultExportJson;
                        Global.DefaultExportObj = settings.DefaultExportObj;
                        Global.DefaultExportPlaintext = settings.DefaultExportPlaintext;

                        Global.PrintLine("Loaded Settings from File.");
                    }
                }
                else
                {
                    Global.SkipMissingGeometry = true;
                    Global.DefaultExportAll = true;
                    Global.DefaultExportRegMap = true;
                    Global.DefaultExportGeometry = true;
                    Global.DefaultExportJson = true;
                    Global.DefaultExportObj = true;
                    Global.DefaultExportPlaintext = true;

                    Global.PrintLine("Initialized default settings.");
                }
            }
            catch (Exception ex)
            {
                Global.PrintLine("Error loading settings: " + ex.Message);
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
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is RegMap selectedRegMap)
            {
                _viewModel.SelectedRegMap = selectedRegMap;
            }
        }

        private void LoadRegMap(string filePath)
        {
            var regMap = RegMapManager.Load(filePath); // Assuming RegMapManager.Load returns a RegMap object
            if (regMap != null && regMap.map != -1)
            {
                _viewModel.RegMapsViewModel.RegMaps.Add(regMap);  // Add to ObservableCollection
                Global.PrintLine($"Loaded: {regMap.alias}");
            }
            else
            {
                Global.PrintLine($"Failed to load: {Path.GetFileName(filePath)}");
            }
        }

        private void OnExportAllClick(object? sender, RoutedEventArgs e)
        {
            var exportDialog = new ExportDialog
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            exportDialog.ShowDialog(this);
        }

        private void OnSettingsClick(object? sender, RoutedEventArgs e)
        {
            var exportDialog = new SettingsDialog
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            exportDialog.ShowDialog(this);
        }
    }
}