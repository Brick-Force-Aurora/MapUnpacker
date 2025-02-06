using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MapUnpackerMVVM.ViewModels;
using MapUnpacker;
using System.IO;
using Newtonsoft.Json;
using System;
using Avalonia.Controls.Shapes;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace MapUnpackerMVVM.Views;

public partial class ExportDialog : UserControl
{
    // Parameterless constructor for XAML designer support
    public ExportDialog()
    {
        InitializeComponent();
        _viewModel = new ExportDialogViewModel();  // Fallback for designer/XAML
        DataContext = _viewModel;
    }

    private readonly ExportDialogViewModel _viewModel;
    public ExportDialog(ExportDialogViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        // Hide the dialog
        this.IsVisible = false;
    }

    private string CreateExportDirectory(string name, int id)
    {
        Directory.CreateDirectory("Export");
        string path = "Export\\" + name + "-" + id;
        Directory.CreateDirectory(path);
        Directory.CreateDirectory(path + "\\OBJ");
        return path;
    }

    private string FilterPathName(string name)
    {
        if (name == "" || name == " ")
            name = "empty";
        else
        {
            name = name.Replace('<', '_');
            name = name.Replace('>', '_');
            name = name.Replace(':', '_');
            name = name.Replace('\"', '_');
            name = name.Replace('/', '_');
            name = name.Replace('\\', '_');
            name = name.Replace('|', '_');
            name = name.Replace('?', '_');
            name = name.Replace('*', '_');
        }

        return name;
    }

    private string FixJSONFormat(string json)
    {
        //fix the format, not an ideal solution
        json = json.Replace("\":{\"", "\":\"");
        json = json.Replace("\"X\":", "\"X=");
        json = json.Replace("\"Y\":", "Y=");
        json = json.Replace("\"Z\":", "Z=");
        json = json.Replace("\"P\":", "\"P=");
        json = json.Replace("\"R\":", "R=");
        json = json.Replace(",R", " R");
        json = json.Replace(",Y", " Y");
        json = json.Replace(",Z", " Z");
        json = json.Replace("},\"", "\",\"");
        json = json.Replace("}}]", "\"}]");
        json = json.Replace("}},{\"", "\"},{\"");
        json = json.Remove(0, 1);
        return json;
    }

    private async void OnExportClick(object? sender, RoutedEventArgs e)
    {
        ExportSpinner.IsActive = true; // Show loading indicator
        ExportSpinner.IsVisible = true;
        ButtonExport.IsEnabled = false;
        ExitButton.IsEnabled = false;
        if (_viewModel.RegMaps.Count <= 0)
        {
            Global.Print("No maps selected for export!");
        } else
        {
            Global.Print(_viewModel.RegMaps.Count + " maps will be exported!");
        }
        await Task.Run(() =>
        {
            foreach (RegMap map in _viewModel.RegMaps)
            {
                if (map == null)
                {
                    continue;
                }
                if (_viewModel.ExportAll || map.isSelected)
                {
                    if (_viewModel.ExportJson)
                    {
                        string json = JsonConvert.SerializeObject(Converter.RegMapToBFRE(map));
                        MapHeader mapData = new MapHeader(map);
                        string mapHeader = JsonConvert.SerializeObject(mapData);
                        mapHeader = mapHeader.Remove(0, 1);
                        json = FixJSONFormat(json);
                        string exportString = "{\"MapData\":{" + mapHeader + "," + json;
                        string name = FilterPathName(map.alias);
                        string path = CreateExportDirectory(name, map.map);
                        File.WriteAllText(path + "\\" + name + ".json", exportString);
                        try
                        {
                            map.thumbnail.Save(path + "\\IMG-" + name + ".png");
                        }

                        catch (Exception ex)
                        {
                            Global.Print("-------------------");
                            Global.Print("Could not save thumbnail");
                            Global.Print("-------------------");
                        }

                        Global.Print("Path: " + path);
                        Global.Print("Json Exported!");
                    }

                    if (_viewModel.ExportObj)
                    {
                        string obj = Converter.RegMapToOBJ(map);
                        string name = FilterPathName(map.alias);
                        int id = map.map;

                        string path = CreateExportDirectory(name, id);

                        //save textures
                        for (int i = 0; i < ObjParser.referencedTextures.Count; i++)
                        {
                            File.WriteAllBytes(path + "\\" + ObjParser.referencedTextures[i].name, ObjParser.referencedTextures[i].data);
                            Global.Print("Saved " + ObjParser.referencedTextures[i].name);
                        }

                        //save mtl
                        File.WriteAllText(path + "\\" + name + ".mtl", ObjParser.mtlString);
                        Global.Print("Saved " + name + ".mtl");

                        //save obj
                        File.WriteAllText(path + "\\" + name + ".obj", obj);
                        Global.Print("Saved " + name + ".obj");

                        Global.Print("Path: " + path);

                        Global.Print("OBJ Exported!");

                        //clean up
                        GC.Collect();
                        ObjParser.referencedTextures.Clear();
                        ObjParser.loadedObjs.Clear();
                    }

                    if (_viewModel.ExportRegMap)
                    {
                    }

                    if (_viewModel.ExportGeometry)
                    {
                    }

                    if (_viewModel.Plaintext)
                    {
                    }
                }
            }
        });

        ExportSpinner.IsActive = false;  // Hide loading indicator after export\
        ExportSpinner.IsVisible = false;
        ButtonExport.IsEnabled = true;
        ExitButton.IsEnabled = true;

        //this.IsVisible = false; // Hide the dialog after export
    }
}