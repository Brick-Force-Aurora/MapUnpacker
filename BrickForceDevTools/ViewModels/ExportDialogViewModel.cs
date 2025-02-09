using BrickForceDevTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickForceDevTools.ViewModels
{
    public class ExportDialogViewModel : ViewModelBase
    {
        public RegMapsViewModel RegMapsViewModel { get; } = RegMapsViewModel.Instance;

        private bool _exportRegMap = true;
        public bool ExportRegMap
        {
            get => _exportRegMap;
            set => SetProperty(ref _exportRegMap, value);
        }

        private bool _exportGeometry = true;
        public bool ExportGeometry
        {
            get => _exportGeometry;
            set => SetProperty(ref _exportGeometry, value);
        }

        private bool _exportJson = true;
        public bool ExportJson
        {
            get => _exportJson;
            set => SetProperty(ref _exportJson, value);
        }

        private bool _exportObj = true;
        public bool ExportObj
        {
            get => _exportObj;
            set => SetProperty(ref _exportObj, value);
        }

        private bool _exportAll = true;
        public bool ExportAll
        {
            get => _exportAll;
            set => SetProperty(ref _exportAll, value);
        }

        private bool _plaintext = true;
        public bool Plaintext
        {
            get => _plaintext;
            set => SetProperty(ref _plaintext, value);
        }

        public bool IsExporting { get; set; }

        public ExportDialogViewModel()
        {
            LoadGlobalSettings();
        }

        public void LoadGlobalSettings()
        {
            ExportRegMap = Global.DefaultExportRegMap;
            ExportGeometry = Global.DefaultExportGeometry;
            ExportJson = Global.DefaultExportJson;
            ExportObj = Global.DefaultExportObj;
            ExportAll = Global.DefaultExportAll;
            Plaintext = Global.DefaultExportPlaintext;
        }
    }
}
