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

        public string TextBase { get => stt_text.Text; set { stt_text.Text = value; txt_text.Text = value; } }
        public TextWrapping TextWrapping { get => stt_text.TextWrapping; set { stt_text.TextWrapping = value; txt_text.TextWrapping = value; } }
        public bool AcceptReturn { get => txt_text.AcceptsReturn; set => txt_text.AcceptsReturn = value; }

        #endregion

        #region Constructors

        public EditableTextBlock()
        {
            InitializeComponent();
            SetLinks();
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

        private void SetLinks()
        {
            this.MouseDoubleClick += (sender, e) => ShowEditor();
            txt_text.LostFocus += (sender, e) => HideEditor();
        }

        private void ShowEditor()
        {
            txt_text.Visibility = Visibility.Visible;
            txt_text.SelectAll();
            txt_text.Focus();
        }

        private void HideEditor()
        {
            txt_text.Visibility = Visibility.Hidden;
            Text = txt_text.Text;
        }

        #endregion
    }
}
