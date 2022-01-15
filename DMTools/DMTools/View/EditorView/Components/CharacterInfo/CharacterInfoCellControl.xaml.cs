using DMTools.Models.SettingModels;
using DMTools.Repositories;
using DMTools.Services;
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

namespace DMTools.View.EditorView.Components
{
    /// <summary>
    /// Interação lógica para CharacterInfoCellControl.xam
    /// </summary>
    public partial class CharacterInfoCellControl : UserControl
    {
        #region Variables and Properties

        CharacterRepository CharRepository => CharacterRepository.GetInstance();

        CharacterInfoControlModel m_vm;
        Action m_onChanged;
        bool isEditorMode = false;

        #endregion

        #region Constructors

        public CharacterInfoCellControl(ObjectInfoModel model, Action onChanged)
        {
            InitializeComponent();
            m_onChanged = onChanged;
            m_vm = new CharacterInfoControlModel(model);
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            SetActions();
            SetCharacters();
            Update(model);
            UpdateControl();
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(m_vm.TXT_Info)) txt_info.Text = m_vm.TXT_Info;
            else if (e.PropertyName == nameof(m_vm.Character)) SetCharacter();
        }

        private void SetActions()
        {
            btn_save.Click += (sender, e) => SaveCharacter();
            btn_edit.Click += (sender, e) => { isEditorMode = true; UpdateControl(); };
        }

        #endregion

        #region Functions

        private void UpdateControl()
        {
            if (isEditorMode) SetVisibility(Visibility.Collapsed, Visibility.Visible);
            else SetVisibility(Visibility.Visible, Visibility.Collapsed);
        }

        private void SetVisibility(Visibility standard, Visibility editor)
        {
            stt_Text.Visibility = standard;
            btn_edit.Visibility = standard;
            txt_info.Visibility = editor;
            cbo_character.Visibility = editor;
            stt_char.Visibility = editor;
            stt_info.Visibility = editor;
            btn_save.Visibility = editor;
        }

        private void SaveCharacter()
        {
            m_vm.TXT_Info = txt_info.Text;
            var cboItem = cbo_character.SelectedItem as ComboBoxItem;
            if (cboItem != null)
            {
                var c = cboItem.Tag as CharacterModel;
                if (c != null) m_vm.Character = c;
            }
            m_onChanged?.Invoke();
            isEditorMode = false;
            UpdateControl();
        }

        private void SetCharacters()
        {
            var chars = CharRepository.Objects.OrderBy(x => x.Name).ToList();
            cbo_character.Items.Clear();
            foreach (CharacterModel c in chars)
            {
                ComboBoxItem cboItem = new ComboBoxItem();
                cboItem.Content = c.Name;
                cboItem.Tag = c;
                cbo_character.Items.Add(cboItem);
            }
            SetCharacter();
        }

        private void SetCharacter()
        {
            if (m_vm.Character == null)
            {
                cbo_character.SelectedItem = null;
                return;
            }
            for (int i = 0; i < cbo_character.Items.Count; i++)
            {
                var cboItem = cbo_character.Items[i] as ComboBoxItem;
                var c = cboItem.Tag as CharacterModel;
                if (c == null) continue;
                if (c.ID != m_vm.Character.ID) continue;
                cbo_character.SelectedItem = cboItem;
                return;
            }
            cbo_character.SelectedItem = null;
        }

        public void Update(ObjectInfoModel obj)
        {
            m_vm.Update(obj);
            isEditorMode = false;
            UpdateControl();
            UpdateSTT(obj);
        }

        private void UpdateSTT(ObjectInfoModel obj)
        {
            stt_Text.Inlines.Clear();
            stt_Text.Inlines.AddRange(FlowDocumentService.GetCharacterInfoRuns(obj));
        }

        #endregion
    }
}
