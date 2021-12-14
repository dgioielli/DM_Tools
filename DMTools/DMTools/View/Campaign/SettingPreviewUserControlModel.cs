using DMTools.CoreLib.ViewModel;
using DMTools.Managers;
using DMTools.Managers.Observers;
using DMTools.Models;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.View.CharacterEditor;
using DMTools.View.ContentViewer;
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
        CharacterRepository CharRepository => CharacterRepository.GetInstance();
        CampaignSettingModel Model => Repository.Model.Setting;

        private List<CharacterModel> m_lstCharactersPage = new List<CharacterModel>();
        private int NumNPCPages => (Model.Characters.Count / 10) + 1;
        private int m_page = 0;

        private Visibility m_txt_visibilitySettingName = Visibility.Hidden;

        protected int Page { get => m_page; set { m_page = value; Update(); } }

        public string TXT_SettingName { get => Model.SettingName; set { Model.SettingName = value; OnPropertyChanged(); } }
        public Visibility TXT_VisibilitySettingName { get => m_txt_visibilitySettingName; set { m_txt_visibilitySettingName = value; OnPropertyChanged(); } }

        public List<CharacterModel> LST_Characters { get => m_lstCharactersPage; }

        public ICommand BTN_EditSettingName { get; protected set; }
        public ICommand BTN_NPCsPreviosPage { get; protected set; }
        public ICommand BTN_NPCsNextPage { get; protected set; }
        public ICommand BTN_NewNPCs { get; protected set; }
        public ICommand BTN_ShowNPCs { get; protected set; }

        #endregion

        #region Constructors

        public SettingPreviewUserControlModel()
        {
            Update();
        }

        #endregion

        #region Functions

        public void Update()
        {
            SetCharacterPage();
            OnPropertyChanged(nameof(LST_Characters));
        }

        private void SetCharacterPage()
        {
            m_lstCharactersPage.Clear();
            int startIndex = 10 * m_page;
            for (int i = 0; i < 10; i++)
            {
                if (Model.Characters.Count <= i + startIndex) break;
                m_lstCharactersPage.Add(Model.Characters[i + startIndex]);
            }
        }

        protected override void assinarComandos()
        {
            BTN_EditSettingName = new DGCommand(obj => TXT_VisibilitySettingName = Visibility.Visible);
            BTN_NPCsNextPage = new DGCommand(obj => Page = (m_page + 1) % NumNPCPages);
            BTN_NPCsPreviosPage = new DGCommand(obj => Page = m_page == 0 ? NumNPCPages - 1 : m_page - 1);
            BTN_NewNPCs = new DGCommand(obj => NewCharacter());
            base.assinarComandos();
        }

        private void NewCharacter()
        {
            var dlg = new CharacterEditorView(CharRepository.GetNewCharacter());
            dlg.Show();
        }

        #endregion
    }
}
