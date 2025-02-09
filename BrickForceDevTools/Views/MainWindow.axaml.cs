using Avalonia.Controls;
using BrickForceDevTools;
using System.IO;
using System;
using System.Collections.ObjectModel;
using BrickForceDevTools.ViewModels;
using Avalonia.Interactivity;
using Newtonsoft.Json;
using System.Collections.Generic;
using Avalonia.Platform.Storage;
using Avalonia;
using Avalonia.VisualTree;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Models;
using MsBox.Avalonia;
using System.Linq;
using System.Text.Json;
using System.Text;
using Tmds.DBus.Protocol;
using System.Reflection.PortableExecutable;

namespace BrickForceDevTools.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        private readonly char crypt = 'E';

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
                    var settings = System.Text.Json.JsonSerializer.Deserialize<MainWindowViewModel>(json);

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

                _viewModel.SkipMissingGeometry = Global.SkipMissingGeometry;
                _viewModel.DefaultExportAll = Global.DefaultExportAll;
                _viewModel.DefaultExportRegMap = Global.DefaultExportRegMap;
                _viewModel.DefaultExportGeometry = Global.DefaultExportGeometry;
                _viewModel.DefaultExportJson = Global.DefaultExportJson;
                _viewModel.DefaultExportObj = Global.DefaultExportObj;
                _viewModel.DefaultExportPlaintext = Global.DefaultExportPlaintext;
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
                _viewModel.RegMapsViewModel.RegMaps.Clear();
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

        private async void OnTemplateFileSelected(object? sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is TemplateFile selectedTemplateFile)
            {
                _viewModel.SelectedTemplateFile = selectedTemplateFile;
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

        private void OnSaveClick(object? sender, RoutedEventArgs e)
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(_viewModel, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(Global.settingsFilePath, json);

                // Update global settings
                Global.SkipMissingGeometry = _viewModel.SkipMissingGeometry;
                Global.DefaultExportAll = _viewModel.DefaultExportAll;
                Global.DefaultExportRegMap = _viewModel.DefaultExportRegMap;
                Global.DefaultExportGeometry = _viewModel.DefaultExportGeometry;
                Global.DefaultExportJson = _viewModel.DefaultExportJson;
                Global.DefaultExportObj = _viewModel.DefaultExportObj;
                Global.DefaultExportPlaintext = _viewModel.DefaultExportPlaintext;

                Global.PrintLine("Settings saved successfully.");
            }
            catch (Exception ex)
            {
                Global.PrintLine($"Error saving settings: {ex.Message}");
            }
        }

        private async void OnClearFoldersClick(object? sender, RoutedEventArgs e)
        {
            try
            {
                var folderPath = Path.GetFullPath("Resources\\Maps");
                var window = this.GetVisualRoot() as Window;

                if (window != null)
                {
                    var messageBox = MessageBoxManager.GetMessageBoxCustom(
                        new MessageBoxCustomParams
                        {
                            ButtonDefinitions = new List<ButtonDefinition>
                            {
                            new ButtonDefinition { Name = "Yes" },
                            new ButtonDefinition { Name = "No" },
                            },
                            ContentTitle = "Confirm Deletion",
                            ContentMessage = $"Are you sure you want to delete files in: {folderPath}?",

                            // Centering and Sizing
                            WindowStartupLocation = WindowStartupLocation.CenterOwner,
                            ShowInCenter = true,
                            CanResize = false,
                            MaxWidth = 500,
                            MaxHeight = 250,
                            SizeToContent = SizeToContent.WidthAndHeight,
                            Topmost = true,

                            // Background & Styling
                            WindowIcon = new WindowIcon("Assets/bf-logo.ico"), // Set a custom window icon
                        });
                    var result = await messageBox.ShowAsPopupAsync(window);
                    if (result != "Yes")
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

                Global.PrintLine("Selected folder cleaned successfully.");
            }
            catch (Exception ex)
            {
                Global.PrintLine($"Error clearing folders: {ex.Message}");
            }
        }

        private async void LoadFiles(object sender, RoutedEventArgs e)
        {
            var folders = await this.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = "Select Folder Containing Template Files",
                AllowMultiple = false
            });

            if (folders.Count > 0)
            {
                var folderPath = folders[0].Path.LocalPath;
                var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                                     .Select(f => new TemplateFile { FileName = Path.GetFileName(f), FileType = Path.GetExtension(f), FilePath = f })
                                     .ToList();
                _viewModel.RegMapsViewModel.TemplateFiles.Clear();
                foreach (var file in files)
                {
                    if (file.FileType == ".cooked")
                    {
                        UncookFile(file);
                    }
                    else if (file.FileType == ".txt")
                    {
                        file.Content = File.ReadAllLines(file.FilePath).Select(line => line.Split(',')).ToList();
                    }
                    _viewModel.RegMapsViewModel.TemplateFiles.Add(file);
                }
            }
        }

        private async void SaveCookedFile(object sender, RoutedEventArgs e)
        {
            var folders = await this.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = "Select Folder To Save Cooked Template Files",
                AllowMultiple = false
            });

            foreach (TemplateFile file in _viewModel.RegMapsViewModel.TemplateFiles)
            {
                if (file.FileType == ".cooked")
                {
                    //copy file to new location
                    var targetPath = Path.Combine(folders[0].Path.LocalPath, file.FileName);
                    File.Copy(file.FilePath, targetPath, true);
                    Global.PrintLine($"Copied Cooked file: {targetPath}");
                } else if (file.FileType == ".txt")
                {
                    var targetPath = Path.Combine(folders[0].Path.LocalPath, file.FileName + ".cooked");
                    CookAndSaveFile(file, targetPath);
                }
            }
        }

        private async void SaveUncookedFile(object sender, RoutedEventArgs e)
        {
            var folders = await this.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = "Select Folder To Save Uncooked Template Files",
                AllowMultiple = false
            });

            foreach (TemplateFile file in _viewModel.RegMapsViewModel.TemplateFiles)
            {
                if (file.FileType == ".cooked")
                {
                    var targetPath = Path.Combine(folders[0].Path.LocalPath, file.FileName.Replace(".cooked", ""));
                    File.WriteAllText(targetPath, string.Join("\n", file.Content.Select(row => string.Join(",", row))));
                    Global.PrintLine($"Saved Uncooked file: {targetPath}");
                }
                else if (file.FileType == ".txt")
                {
                    //copy file to new location
                    var targetPath = Path.Combine(folders[0].Path.LocalPath, file.FileName);
                    File.Copy(file.FilePath, targetPath, true);
                    Global.PrintLine($"Copied Uncooked file: {targetPath}");
                }
            }
        }

        private void UncookFile(TemplateFile file)
        {
            if (file == null) return;
            using (FileStream fileStream = File.Open(file.FilePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                int num = reader.ReadInt32();
                for (int i = 0; i < num; i++)
                {
                    int num2 = reader.ReadInt32();
                    string[] array = new string[num2];
                    for (int j = 0; j < num2; j++)
                    {
                        int num3 = reader.ReadInt32();
                        if (num3 > 0)
                        {
                            char[] array2 = reader.ReadChars(num3);
                            for (int k = 0; k < num3; k++)
                                array2[k] ^= crypt;
                            array[j] = new string(array2);
                        }
                        else
                        {
                            array[j] = string.Empty;
                        }
                    }
                    file.Content.Add(array);
                }
                reader.Close();
                fileStream.Close();
            }
            Global.PrintLine($"Uncooked file: {file.FilePath}");
        }

        private void CookAndSaveFile(TemplateFile file, string pathName)
        {
            if (file == null) return;
            using (FileStream fileStream = File.Open(pathName, FileMode.Create, FileAccess.Write))
            using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
            {
                binaryWriter.Write(file.Content.Count);
                foreach (var row in file.Content)
                {
                    binaryWriter.Write(row.Length);
                    foreach (var entry in row)
                    {
                        char[] array = entry.ToCharArray();
                        for (int k = 0; k < array.Length; k++)
                            array[k] ^= crypt;
                        binaryWriter.Write(array.Length);
                        binaryWriter.Write(array);
                    }
                }
                binaryWriter.Close();
                fileStream.Close();
            }
            Global.PrintLine($"Saved Cooked file: {pathName}");
        }
    }
}