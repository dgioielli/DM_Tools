using DMTools.CoreLib.PoolItems;
using DMTools.Managers;
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
        CharacterRepository CharRepository => CharacterRepository.GetInstance();
        PoolGeneric<ObjectSettingCellControl, ObjectSettingModel> m_poolCharacters;

        SettingPreviewUserControlModel m_vm = new SettingPreviewUserControlModel();

        #endregion

        #region Constructors

        public SettingPreviewUserControl()
        {
            InitializeComponent();
            Observer.RegisterGeneralObserver(m_vm);
            m_poolCharacters = new PoolGeneric<ObjectSettingCellControl, ObjectSettingModel>(newCharacter, refreshCharacter);
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
            var list = m_poolCharacters.GetObjects(getObjects(m_vm.LST_Characters));
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

        private List<ObjectSettingModel> getObjects(List<CharacterModel> lST_Characters)
        {
            var result = new List<ObjectSettingModel>();
            lST_Characters.ForEach(x => result.Add(new ObjectSettingModel(x, updateCharacter, deleteCharacter, duplicateCharacter, showContentCharacter)));
            return result;
        }

        private ObjectSettingCellControl refreshCharacter(ObjectSettingCellControl arg1, ObjectSettingModel arg2)
        {
            arg1.Update(arg2);
            return arg1;
        }

        private ObjectSettingCellControl newCharacter(ObjectSettingModel arg) => new ObjectSettingCellControl(arg);

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

        private void showContentCharacter(string obj)
        {
            var dlg = new ContentViewerView(new ContentViewerCharacterModel(CharRepository.GetCharacterById(obj)));
            dlg.Show();
        }

        private void duplicateCharacter(string obj)
        {
            var character = CharRepository.GetCharacterById(obj);
            if (character == null) return;
            if (MessageBox.Show($"Do you want to permanetly delete the section \"{character.Name}\"?", "DM Tools", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            CharRepository.DeleteCharacter(character);
        }

        private void deleteCharacter(string obj)
        {
            var dlg = new CharacterEditorView(CharRepository.GetCopy(CharRepository.GetCharacterById(obj)));
            dlg.Show();
        }

        private void updateCharacter(string obj)
        {
            var dlg = new CharacterEditorView(CharRepository.GetDuplicate(CharRepository.GetCharacterById(obj)));
            dlg.Show();
        }

        #endregion
    }
}
