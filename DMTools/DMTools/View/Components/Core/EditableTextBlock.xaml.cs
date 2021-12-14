using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interação lógica para EditableTextBlock.xam
    /// </summary>
    public partial class EditableTextBlock : UserControl
    {
        #region Variables and Properties

        public Action OnTextChanged;

        private string m_placeHolder = "";

        public string TextBase { get => txt_text.Text; set { txt_text.Text = value; setLabel(value); } }
        public TextWrapping TextWrapping { get => stt_text.TextWrapping; set { stt_text.TextWrapping = value; txt_text.TextWrapping = value; } }
        public bool AcceptReturn { get => txt_text.AcceptsReturn; set => txt_text.AcceptsReturn = value; }
        public string PlaceHolder { get => m_placeHolder; set { m_placeHolder = value; setLabel(TextBase); } }

        #endregion

        #region Constructors

        public EditableTextBlock()
        {
            InitializeComponent();
            SetLinks();
            setLabel("");
        }

        #endregion

        #region Bindable
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(EditableTextBlock), new PropertyMetadata( "", TextPropertyChanged));

        private static void TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((EditableTextBlock)d).TextPropertyChanged((string)e.NewValue);

        private void TextPropertyChanged(string value) => TextBase = value; 
        
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region Functions

        private void setLabel(string value)
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

        private void SetLinks()
        {
            this.MouseDoubleClick += (sender, e) => ShowEditor();
            txt_text.LostFocus += (sender, e) => HideEditor();
        }

        private void ShowEditor()
        {
            txt_text.MinHeight = stt_text.ActualHeight;
            txt_text.Visibility = Visibility.Visible;
            txt_text.SelectAll();
            txt_text.Focus();
        }

        private void HideEditor()
        {
            txt_text.MinHeight = 0.0;
            txt_text.Visibility = Visibility.Hidden;
            TextBase = txt_text.Text;
            Text = txt_text.Text;
            OnTextChanged?.Invoke();
        }

        #endregion
    }
}
