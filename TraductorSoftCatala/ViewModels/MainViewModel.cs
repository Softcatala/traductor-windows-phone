/*
 * Copyright (C) 2014 Bernat Mut <berni.emerald@gmail.com>
 * 
 * This file is part of Traductor Softcatala.
 * Traductor Softcatala is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License as
 * published by the Free Software Foundation; either version 2 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public
 * License along with this program; if not, write to the
 * Free Software Foundation, Inc., 59 Temple Place - Suite 330,
 * Boston, MA 02111-1307, USA.
 */
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using TraductorSoftcatala.Resources;

namespace TraductorSoftcatala.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.LanguageList = new ObservableCollection<LanguageViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<LanguageViewModel> LanguageList { get; private set; }

        public string AppVersion
        {
            get
            {
                var nameHelper = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
                var version = nameHelper.Version;
                return version.ToString();

            }
        }


        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        /// 
        public void LoadData()
        {

            // Setting Ads 
            MsVisibilityResult = true;
#if DEBUG
            MsAdUnitId = "Image480_80";
            MsApplicationId = "test_client";
#else
            MsAdUnitId = "10732315";
            MsApplicationId = "29b641b5-076b-4e7c-9804-4e2c7de60c88";

#endif

            
            LanguageList.Add(new LanguageViewModel { LangCode = "ca|es", Language = AppResources.ca_es });
            LanguageList.Add(new LanguageViewModel { LangCode = "ca|en", Language = AppResources.ca_en });
            LanguageList.Add(new LanguageViewModel { LangCode = "ca|fr", Language = AppResources.ca_fr });
            LanguageList.Add(new LanguageViewModel { LangCode = "ca|pt", Language = AppResources.ca_pt });
            LanguageList.Add(new LanguageViewModel { LangCode = "en|ca", Language = AppResources.en_ca });
            LanguageList.Add(new LanguageViewModel { LangCode = "es|ca", Language = AppResources.es_ca });
            LanguageList.Add(new LanguageViewModel { LangCode = "fr|ca", Language = AppResources.fr_ca });
            LanguageList.Add(new LanguageViewModel { LangCode = "pt|ca", Language = AppResources.pt_ca });



            ValenciaSelected = false;

            this.IsDataLoaded = true;


        }


        Boolean _valenciaVisible;
        public Boolean ValenciaVisible
        {
            get
            {
                return _valenciaVisible;
            }
            set
            {
                if (value != _valenciaVisible)
                {
                    _valenciaVisible = value;
                    NotifyPropertyChanged("ValenciaVisible");
                }
            }
        }
        Boolean _valenciaSelected;
        public Boolean ValenciaSelected
        {
            get
            {
                return _valenciaSelected;
            }
            set
            {
                if (value != _valenciaSelected)
                {
                    _valenciaSelected = value;
                    NotifyPropertyChanged("ValenciaSelected");
                }
            }
        }



        string _msAdUnitId;
        public string MsAdUnitId
        {
            get
            {
                return _msAdUnitId;
            }
            set
            {
                if (value != _msAdUnitId)
                {
                    _msAdUnitId = value;
                    NotifyPropertyChanged("MsAdUnitId");
                }
            }
        }


        string _msApplicationId;
        public string MsApplicationId
        {
            get
            {
                return _msApplicationId;
            }
            set
            {
                if (value != _msApplicationId)
                {
                    _msApplicationId = value;
                    NotifyPropertyChanged("MsApplicationId");
                }
            }
        }



        Boolean _msVisibilityResult;
        public Boolean MsVisibilityResult
        {
            get
            {
                return _msVisibilityResult;
            }
            set
            {
                if (value != _msVisibilityResult)
                {
                    _msVisibilityResult = value;
                    NotifyPropertyChanged("MsVisibilityResult");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}