using MapUnpacker;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.ComponentModel;

namespace MapUnpackerMVVM.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public RegMapsViewModel RegMapsViewModel { get; } = RegMapsViewModel.Instance;

        private RegMap _selectedRegMap;
        public RegMap SelectedRegMap
        {
            get => _selectedRegMap;
            set
            {
                if (SetProperty(ref _selectedRegMap, value))
                {
                    UpdateMapDetails(value);
                }
            }
        }

        private void UpdateMapDetails(RegMap selectedRegMap)
        {
            if (selectedRegMap != null)
            {
                RegMapsViewModel.MapName = selectedRegMap.alias;
                RegMapsViewModel.MapId = selectedRegMap.map.ToString();
                RegMapsViewModel.Creator = selectedRegMap.developer;
                RegMapsViewModel.Date = selectedRegMap.regDate.ToString();
                RegMapsViewModel.BrickCount = selectedRegMap.geometry.brickCount;
                RegMapsViewModel.Modes = selectedRegMap.modeMask.ToString();
                RegMapsViewModel.IsOfficial = selectedRegMap.officialMap;
                RegMapsViewModel.IsClan = selectedRegMap.clanMatchable;
                RegMapsViewModel.Thumbnail = selectedRegMap.thumbnail;
            }
        }
    }
}
