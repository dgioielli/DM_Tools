using DMTools.CoreLib.ViewModel;
using DMTools.Managers;
using DMTools.Managers.Observers;
using DMTools.Models;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.View.CharacterEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DMTools.View.Campaign
{
    class ObjectBaseCarouselControlModel : DGViewModel, IGeneralObserver
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();

        private List<IObjectBase> m_lstPage = new List<IObjectBase>();
        private int m_page = 0;

        private int NumPages => (GetObjects().Count / 10) + 1;

        protected int PageNPC { get => m_page; set { m_page = value; Update(); } }

        public List<IObjectBase> LST_Objects { get => m_lstPage; }

        public ICommand BTN_PreviosPage { get; protected set; }
        public ICommand BTN_NextPage { get; protected set; }
        public ICommand BTN_New { get; protected set; }
        public ICommand BTN_Show { get; protected set; }

        Func<List<IObjectBase>> GetObjects;
        Action NewObject;
        Action ShowListing;

        #endregion

        #region Constructors

        public ObjectBaseCarouselControlModel(Func<List<IObjectBase>> getObjects, Action newObject, Action showListing)
        {
            GetObjects = getObjects;
            NewObject = newObject;
            ShowListing = showListing;
            Update();
        }

        #endregion

        #region Functions

        public void Update()
        {
            SetPage();
            OnPropertyChanged(nameof(LST_Objects));
        }

        private void SetPage()
        {
            m_lstPage.Clear();
            int startIndex = 10 * m_page;
            var list = GetObjects();
            for (int i = 0; i < 10; i++)
            {
                if (list.Count <= i + startIndex) break;
                m_lstPage.Add(list[i + startIndex]);
            }
        }

        protected override void assinarComandos()
        {
            BTN_NextPage = new DGCommand(obj => PageNPC = (m_page + 1) % NumPages);
            BTN_PreviosPage = new DGCommand(obj => PageNPC = m_page == 0 ? NumPages - 1 : m_page - 1);
            BTN_New = new DGCommand(obj => NewObject());
            BTN_Show = new DGCommand(obj => ShowListing());
            base.assinarComandos();
        }

        #endregion
    }
}
