using MapUnpacker.Properties;
using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace MapUnpacker
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();

            //initialize material skin
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey900, Primary.Lime600, Primary.Grey900, Accent.Lime400, TextShade.WHITE);
            materialSkinManager.ROBOTO_MEDIUM_10 = new Font("Tahoma", 10);
            materialSkinManager.ROBOTO_MEDIUM_11 = new Font("Tahoma", 11);
            materialSkinManager.ROBOTO_MEDIUM_12 = new Font("Tahoma", 12);
            materialSkinManager.ROBOTO_REGULAR_11 = new Font("Tahoma", 9);

            //get brick data collection
            if (File.Exists("..\\DATA\\bricks.json"))
            {
                string json = File.ReadAllText("..\\DATA\\bricks.json");
                RegMapManager.bricks = JsonConvert.DeserializeObject<List<Brick>>(json);
                Converter.BuildAliasToRE();
            } else
            {
                throw new Exception("Resources/bricks.json is missing");
            }
        }

        //access output text field from outside the form
        public void PrintOutput(string text)
        {
            textBoxConsole.AppendText(text + Environment.NewLine);
        }

        //load single map from open file dialog to map collection
        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            openFileDialog.InitialDirectory = "Resources\\Maps";
            openFileDialog.Filter = "RegMap files (*.regmap)|*.regmap";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }

            RegMapManager.curRegMap = RegMapManager.Load(filePath);

            //check if the map was loaded correctly, if yes update collections and gui data
            if (RegMapManager.curRegMap != null && RegMapManager.curRegMap.map != -1)
            {
                RegMapManager.regMaps.Add(RegMapManager.curRegMap);
                comboMapSelection.Items.Add(RegMapManager.curRegMap.alias);
                comboMapSelection.SelectedItem = comboMapSelection.Items[comboMapSelection.Items.IndexOf(RegMapManager.curRegMap.alias)];

                UpdateMetadata();
            }
        }

        //load whole folder of maps from folder browser dialog
        public void buttonLoadAllFiles_Click(object sender, EventArgs e)
        {
            comboMapSelection.Items.Clear();
            RegMapManager.regMaps.Clear();
            var folderPath = string.Empty;
            folderBrowserDialog.SelectedPath = Path.GetFullPath("Resources\\Maps");

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog.SelectedPath;
            }

            try
            {
                string[] files = Directory.GetFiles(folderPath, "*.regmap", SearchOption.AllDirectories);

                for (int i = 0; i < files.Length; i++)
                {
                    //RegMap regMap = new RegMap();
                    RegMap regMap = RegMapManager.Load(files[i]);
                    if (regMap.map != -1)
                    {
                        RegMapManager.regMaps.Add(regMap);
                        comboMapSelection.Items.Add(regMap.alias);
                    }
                }

                if (RegMapManager.regMaps.Count > 0)
                {
                    comboMapSelection.SelectedItem = comboMapSelection.Items[0];
                    RegMapManager.curRegMap = RegMapManager.regMaps[0];
                    UpdateMetadata();
                    Global.Print("Loaded " + RegMapManager.regMaps.Count + " Maps");
                }
            }

            catch
            {
            }
        }

        //update gui map metadata
        private void UpdateMetadata()
        {
            if (RegMapManager.curRegMap.map == -1)
                return;

            pictureThumbnail.Image = RegMapManager.curRegMap.thumbnail;
            labelAliasData.Text = RegMapManager.curRegMap.alias;
            labelMapIdData.Text = RegMapManager.curRegMap.map.ToString();

            if (RegMapManager.curRegMap.geometry.brickCount > 0)
                labelVersionData.Text = RegMapManager.curRegMap.geometry.brickCount.ToString();
            else
                labelVersionData.Text = "NO GEOMETRY";

            labelCreatorData.Text = RegMapManager.curRegMap.developer.ToString();
            labelDateData.Text = RegMapManager.curRegMap.regDate.ToString();
            labelModesData.Text = RegMapManager.curRegMap.modeMask.ToString();
            labelClanData.Text = RegMapManager.curRegMap.clanMatchable.ToString();
            labelOfficialData.Text = RegMapManager.curRegMap.officialMap.ToString();
        }

        //update map selection
        private void comboMapSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegMapManager.curRegMap = RegMapManager.regMaps.Find(x => x.alias == (string)comboMapSelection.SelectedItem);

            UpdateMetadata();
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

        private string CreateExportDirectory(string name)
        {
            Directory.CreateDirectory("Export");
            string path = "Export\\" + name;
            Directory.CreateDirectory(path);
            return path;
        }

        private string CreateExportREDirectory(string name, int id)
        {
            Directory.CreateDirectory("ExportRE");
            string path = "ExportRE\\" + name + "-" + id;
            Directory.CreateDirectory(path);
            return path;
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

        //export BF RE json map file
        private void buttonExport_Click(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(Converter.RegMapToBFRE(RegMapManager.curRegMap));
            MapHeader mapData = new MapHeader(RegMapManager.curRegMap);
            string mapHeader = JsonConvert.SerializeObject(mapData);
            mapHeader = mapHeader.Remove(0, 1);
            json = FixJSONFormat(json);
            string exportString = "{\"MapData\":{" + mapHeader + "," + json;
            string name = FilterPathName(RegMapManager.curRegMap.alias);
            string path = CreateExportREDirectory(name, RegMapManager.curRegMap.map);
            File.WriteAllText(path + "\\" + name + ".json", exportString);
            try
            {
                RegMapManager.curRegMap.thumbnail.Save(path + "\\IMG-" + name + ".png");
            }

            catch (Exception ex)
            {
                Global.Print("-------------------");
                Global.Print("Could not save thumbnail");
                Global.Print("-------------------");
            }

            Global.Print("Path: " + path);
            Global.Print("Done");
        }

        private void buttonExportAll_Click(object sender, EventArgs e)
        {
            int count = 0;
            List<string> badThumbnails = new List<string>();
            foreach (RegMap regMap in RegMapManager.regMaps)
            {
                string json = JsonConvert.SerializeObject(Converter.RegMapToBFRE(regMap));
                MapHeader mapData = new MapHeader(regMap);
                string mapHeader = JsonConvert.SerializeObject(mapData);
                mapHeader = mapHeader.Remove(0, 1);
                json = FixJSONFormat(json);
                string exportString = "{\"MapData\":{" + mapHeader + "," + json;
                string name = FilterPathName(regMap.alias);
                string path = CreateExportREDirectory(name, regMap.map);
                File.WriteAllText(path + "\\" + name + ".json", exportString);
                try
                {
                    regMap.thumbnail.Save(path + "\\IMG-" + name + ".png");
                }

                catch (Exception ex)
                {
                    badThumbnails.Add(regMap.alias + "-" + regMap.map);
                }

                count++;
                Global.Print("Exported " + count + " " + regMap.alias);
            }

            Global.Print("Path: " + "ExportRE\\");
            Global.Print("Exported " + count + " maps");
            if (badThumbnails.Count > 0)
            {
                File.WriteAllLines("errorLog.txt", badThumbnails);
                Global.Print("-------------------");
                Global.Print("Could not save " + badThumbnails.Count + " thumbnails");
                Global.Print("Check errorLog.txt");
                Global.Print("-------------------");
            }
            Global.Print("Done");
            GC.Collect();
        }

        //export obj mesh + mtl & textures (high ram usage, could possibly be reduced)
        private void buttonExportObj_Click(object sender, EventArgs e)
        {
            //get obj string
            string obj = Converter.RegMapToOBJ(RegMapManager.curRegMap);
            string name = FilterPathName(RegMapManager.curRegMap.alias);

            string path = CreateExportDirectory(name);

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

            Global.Print("Done");

            //clean up
            GC.Collect();
            ObjParser.referencedTextures.Clear();
            ObjParser.loadedObjs.Clear();
        }

        //update output window
        private void textBoxConsole_TextChanged(object sender, EventArgs e)
        {
            textBoxConsole.SelectionStart = textBoxConsole.Text.Length;
            textBoxConsole.ScrollToCaret();
        }

        private void checkSkipNoGeometry_CheckedChanged(object sender, EventArgs e)
        {
            Global.skipNoGeometry = !Global.skipNoGeometry;
        }

        //update map selection sorting
        private void comboSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sort = (string)comboSortBy.SelectedItem;
            comboMapSelection.Items.Clear();

            if (sort == "Alias")
                RegMapManager.regMaps = RegMapManager.regMaps.OrderBy(x => x.alias).ToList();

            else if (sort == "Map ID")
                RegMapManager.regMaps = RegMapManager.regMaps.OrderBy(x => x.map).ToList();

            else if (sort == "Bricks")
                RegMapManager.regMaps = RegMapManager.regMaps.OrderByDescending(x => x.geometry.brickCount).ToList();

            else if (sort == "Creator")
                RegMapManager.regMaps = RegMapManager.regMaps.OrderBy(x => x.developer).ToList();

            for (int i = 0; i < RegMapManager.regMaps.Count; i++)
                comboMapSelection.Items.Add(RegMapManager.regMaps[i].alias);

            comboMapSelection.SelectedIndex = 0;
            Global.Print("Sorted by " + sort);
        }

        private void buttonCLeanFolder_Click(object sender, EventArgs e)
        {
            var folderPath = string.Empty;
            folderBrowserDialog.SelectedPath = Path.GetFullPath("Resources\\Maps");

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog.SelectedPath;
            }

            try
            {
                string[] files = Directory.GetFiles(folderPath, "*.regmap", SearchOption.AllDirectories);
                string[] files2 = Directory.GetFiles(folderPath, "*.cache", SearchOption.AllDirectories);

                for (int i = 0; i < files2.Length; i++)
                    File.Delete(files2[i]);

                for (int i = 0; i < files.Length; i++)
                {
                    RegMap regMap = RegMapManager.Load(files[i]);
                    if (regMap.map != -1)
                    {
                        if (regMap.geometry.brickCount == 0)
                        {
                            File.Delete(files[i]);
                        }
                    }
                }
            }

            catch
            {
            }
        }
    }
}
