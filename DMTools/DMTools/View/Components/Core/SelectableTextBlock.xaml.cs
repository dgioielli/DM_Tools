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

namespace DMTools.View.Components.Core
{
    /// <summary>
    /// Interação lógica para SelectableTextBlock.xam
    /// </summary>
    public partial class SelectableTextBlock : UserControl
    {
        #region Variables and Properties

        public Action OnTextChanged;

        public string TextBase { get => cbo_text.Text; set { cbo_text.Text = value; SetLabel(value); } }
        public bool IsEditable { get => cbo_text.IsEditable; set => cbo_text.IsEditable = value; }

        private string m_placeHolder = "";
        public string PlaceHolder { get => m_placeHolder; set { m_placeHolder = value; SetLabel(TextBase); } }

        #endregion

        #region Constructors

        public SelectableTextBlock()
        {
            InitializeComponent();
            SetLinks();
            SetLabel("");
        }

        #endregion

        #region Bindable
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(SelectableTextBlock), new PropertyMetadata("", TextPropertyChanged));

        private static void TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((SelectableTextBlock)d).TextPropertyChanged((string)e.NewValue);

        private void TextPropertyChanged(string value) => TextBase = value;

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region Functions

        private void SetLabel(string value)
        {
            if (value == "")
            {
                stt_text.Foreground = Brushes.Gray;
                stt_text.Text = PlaceHolder;
            }
            else
            {
                stt_text.Foreground = Brushes.Black;
                stt_text.Text = value;
            }
        }

        public void SetOptions(List<string> options)
        {
            cbo_text.Items.Clear();
            options.ForEach(X => cbo_text.Items.Add(X));
        }

        private void SetLinks()
        {
            this.MouseDoubleClick += (sender, e) => ShowEditor();
            cbo_text.LostFocus += (sender, e) => HideEditor();
        }

        private void ShowEditor()
        {
            cbo_text.Visibility = Visibility.Visible;
            var textBox = (cbo_text.Template.FindName("PART_EditableTextBox", cbo_text) as TextBox);
            if (textBox != null)
            {
                textBox.Focus();
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        private void HideEditor()
        {
            cbo_text.MinHeight = 0.0;
            cbo_text.Visibility = Visibility.Hidden;
            TextBase = cbo_text.Text;
            Text = cbo_text.Text;
            OnTextChanged?.Invoke();
        }

        #endregion
    }
}
