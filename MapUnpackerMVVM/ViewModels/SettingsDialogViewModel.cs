using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapUnpackerMVVM.ViewModels
{
    public partial class SettingsDialogViewModel: ObservableObject
    {
        [ObservableProperty] private bool skipMissingGeometry = true;
        [ObservableProperty] private bool defaultExportAll = true;
        [ObservableProperty] private bool defaultExportRegMap = true;
        [ObservableProperty] private bool defaultExportGeometry = true;
        [ObservableProperty] private bool defaultExportJson = true;
        [ObservableProperty] private bool defaultExportObj = true;
        [ObservableProperty] private bool defaultExportPlaintext = true;
    }
}
