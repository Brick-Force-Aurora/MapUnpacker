﻿using MapUnpacker;
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
        private ObservableCollection<RegMap> _regMaps = new ObservableCollection<RegMap>();
        public ObservableCollection<RegMap> RegMaps
        {
            get => _regMaps;
            set
            {
                _regMaps = value;
                OnPropertyChanged(nameof(RegMaps));
            }
        }

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

        private string _mapName;
        public string MapName
        {
            get => _mapName;
            set => SetProperty(ref _mapName, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        private string _mapId;
        public string MapId
        {
            get => _mapId;
            set => SetProperty(ref _mapId, value);
        }

        private Avalonia.Media.Imaging.Bitmap _thumbnail;
        public Avalonia.Media.Imaging.Bitmap Thumbnail
        {
            get => _thumbnail;
            set => SetProperty(ref _thumbnail, value);
        }

        private string _creator;
        public string Creator
        {
            get => _creator;
            set => SetProperty(ref _creator, value);
        }

        private string _date;
        public string Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private int _brickCount;
        public int BrickCount
        {
            get => _brickCount;
            set => SetProperty(ref _brickCount, value);
        }

        private string _modes;
        public string Modes
        {
            get => _modes;
            set => SetProperty(ref _modes, value);
        }

        private bool _isOfficial;
        public bool IsOfficial
        {
            get => _isOfficial;
            set => SetProperty(ref _isOfficial, value);
        }

        private bool _isClan;
        public bool IsClan
        {
            get => _isClan;
            set => SetProperty(ref _isClan, value);
        }

        private void UpdateMapDetails(RegMap selectedRegMap)
        {
            if (selectedRegMap != null)
            {
                MapName = selectedRegMap.alias;
                MapId = selectedRegMap.map.ToString();
                Creator = selectedRegMap.developer;
                Date = selectedRegMap.regDate.ToString();
                BrickCount = selectedRegMap.geometry.brickCount;
                Modes = selectedRegMap.modeMask.ToString();
                IsOfficial = selectedRegMap.officialMap;
                IsClan = selectedRegMap.clanMatchable;
                Thumbnail = selectedRegMap.thumbnail;
            }
        }
    }
}
