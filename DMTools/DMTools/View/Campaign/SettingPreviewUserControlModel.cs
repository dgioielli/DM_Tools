using DMTools.CoreLib.ViewModel;
using DMTools.Managers;
using DMTools.Managers.Observers;
using DMTools.Models;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.View.CharacterEditor;
using DMTools.View.ContentViewer;
using DMTools.View.LocationEditor;
using DMTools.View.OrganizationEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DMTools.View.Campaign
{
    class SettingPreviewUserControlModel : DGViewModel, IGeneralObserver
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        CampaignRepository Repository => CampaignRepository.GetInstance();
        CampaignSettingModel Model => Repository.Model.Setting;

        private Visibility m_txt_visibilitySettingName = Visibility.Hidden;

        public string TXT_SettingName { get => Model.SettingName; set { Model.SettingName = value; OnPropertyChanged(); } }
        public Visibility TXT_VisibilitySettingName { get => m_txt_visibilitySettingName; set { m_txt_visibilitySettingName = value; OnPropertyChanged(); } }

        public ICommand BTN_EditSettingName { get; protected set; }

        #endregion

        #region Constructors

        public SettingPreviewUserControlModel()
        {
            Update();
        }

        #endregion

        #region Functions

        public void Update()
        { }

        #endregion
    }
}
