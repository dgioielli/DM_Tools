using DMTools.Models.SessionModels;
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

namespace DMTools.View.SectionEditor
{
    /// <summary>
    /// Interação lógica para SessionCharacterCellControl.xam
    /// </summary>
    public partial class SessionCharacterCellControl : UserControl
    {
        #region Variables and Properties

        CharacterRepository CharRepository => CharacterRepository.GetInstance();

        SessionCharacterCellControlModel m_vm;
        Action m_onChanged;
        bool isEditorMode = false;

        #endregion

        #region Constructors

        public SessionCharacterCellControl(SessionCharacterModel model, Action onChanged)
        {
            InitializeComponent();
            m_onChanged = onChanged;
            m_vm = new SessionCharacterCellControlModel(model);
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            SetActions();
            SetCharacters();
            LoadCboRole();
            Update(model);
            UpdateControl();
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(m_vm.TXT_Info)) txt_info.Text = m_vm.TXT_Info;
            else if (e.PropertyName == nameof(m_vm.Character)) SetCharacter();
            else if (e.PropertyName == nameof(m_vm.CBO_Index_Role)) cbo_role.SelectedIndex = m_vm.CBO_Index_Role;
        }

        private void SetActions()
        {
            btn_save.Click += (sender, e) => SaveCharacter();
            btn_edit.Click += (sender, e) => { isEditorMode = true; UpdateControl(); };
            cbo_role.SelectionChanged += (sender, e) => m_vm.CBO_Index_Role = cbo_role.SelectedIndex;
        }

        #endregion

        #region Functions

        private void LoadCboRole()
        {
            cbo_role.Items.Add("Aliado");
            cbo_role.Items.Add("Inimigo");
            cbo_role.Items.Add("Neutro");
            cbo_role.Items.Add("Secundário");
        }

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
            stt_role.Visibility = editor;
            cbo_role.Visibility = editor;
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
            var chars = CharRepository.Characters.OrderBy(x => x.Name).ToList();
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

        public void Update(SessionCharacterModel obj)
        {
            m_vm.Update(obj);
            isEditorMode = false;
            UpdateControl();
            UpdateSTT(obj);
        }

        private void UpdateSTT(SessionCharacterModel obj)
        {
            stt_Text.Inlines.Clear();
            stt_Text.Inlines.AddRange(FlowDocumentService.GetSessionCharacterRuns(obj));
        }

        #endregion
    }
}
