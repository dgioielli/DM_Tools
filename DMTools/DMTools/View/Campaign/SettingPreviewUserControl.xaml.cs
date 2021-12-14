using DMTools.CoreLib.PoolItems;
using DMTools.Managers;
using DMTools.Models.SettingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DMTools.View.Campaign
{
    /// <summary>
    /// Interação lógica para SettingPreviewUserControl.xam
    /// </summary>
    public partial class SettingPreviewUserControl : UserControl
    {
        #region Variables and Properties

        ObserverManager Observer => ObserverManager.GetInstance();
        PoolGeneric<CharacterCellControl, CharacterModel> m_poolCharacters;

        SettingPreviewUserControlModel m_vm = new SettingPreviewUserControlModel();

        #endregion

        #region Constructors

        public SettingPreviewUserControl()
        {
            InitializeComponent();
            Observer.RegisterGeneralObserver(m_vm);
            m_poolCharacters = new PoolGeneric<CharacterCellControl, CharacterModel>(newCharacter, refreshCharacter);
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            SetActions();
            ShowCharacters();
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(m_vm.TXT_VisibilitySettingName)) SetTxtSettingNameFocus();
            else if (e.PropertyName == nameof(m_vm.LST_Characters)) ShowCharacters();
        }

        private void SetActions()
        {
            grd_SettingName.MouseEnter += (sender, e) => btn_editSettingName.Visibility = Visibility.Visible;
            grd_SettingName.MouseLeave += (sender, e) => btn_editSettingName.Visibility = Visibility.Collapsed;
            txt_settingName.LostFocus += (sender, e) => m_vm.TXT_VisibilitySettingName = Visibility.Hidden;
        }

        #endregion

        #region Functions

        private void SetTxtSettingNameFocus()
        {
            txt_settingName.Visibility = m_vm.TXT_VisibilitySettingName;
            if (m_vm.TXT_VisibilitySettingName != Visibility.Visible) return;
            txt_settingName.Focus();
            txt_settingName.SelectAll();
        }

        private void ShowCharacters()
        {
            ClearGridCharacters();
            var list = m_poolCharacters.GetObjects(m_vm.LST_Characters);
            for (int i = 0; i < list.Count; i++)
            {
                int col = i % 5;
                int row = i / 5;
                var uc = list[i];
                Grid.SetColumn(uc, col + 1);
                Grid.SetRow(uc, row);
                grd_characters.Children.Add(uc);
            }
        }

        private CharacterCellControl refreshCharacter(CharacterCellControl arg1, CharacterModel arg2)
        {
            arg1.Update(arg2);
            return arg1;
        }

        private CharacterCellControl newCharacter(CharacterModel arg) => new CharacterCellControl(arg);

        private void ClearGridCharacters()
        {
            var children = grd_characters.Children;
            var childrenToRemove = new List<UIElement>();
            for (int i = 0; i < children.Count; i++)
            {
                int col = Grid.GetColumn(children[i]);
                if (col == 0 || col >= 6) continue;
                childrenToRemove.Add(children[i]);
            }
            childrenToRemove.ForEach(x => grd_characters.Children.Remove(x));
        }

        #endregion
    }
}
