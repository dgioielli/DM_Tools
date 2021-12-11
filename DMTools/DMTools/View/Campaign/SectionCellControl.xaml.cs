﻿using DMTools.CoreLib.PoolItems;
using DMTools.Keys;
using DMTools.Models;
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
    /// Interação lógica para SectionCellControl.xam
    /// </summary>
    public partial class SectionCellControl : UserControl, IReusablePool<SectionModel>
    {
        #region Variables and Properties

        SectionCellControlModel m_vm;

        #endregion

        #region Constructors

        public SectionCellControl() : this(new SectionModel())
        { }

        public SectionCellControl(SectionModel model)
        {
            InitializeComponent(); 
            m_vm = new SectionCellControlModel(model);
            DataContext = m_vm;
            m_vm.PropertyChanged += M_vm_PropertyChanged;
            SetActions();
            ShowButtons(false);
        }

        #endregion

        #region Events

        private void M_vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void SetActions()
        {
            this.MouseEnter += (sender, e) => ShowButtons(true);
            this.MouseLeave += (sender, e) => ShowButtons(false);
            this.MouseDoubleClick += (sender, e) => m_vm.ShowContent();
        }

        #endregion

        #region Functions

        public void Update(SectionModel obj) => m_vm.Update(obj);

        private void ShowButtons(bool show)
        {
            var visible = Visibility.Visible;
            if (!show) visible = Visibility.Collapsed;
            btn_edit.Visibility = visible;
            btn_del.Visibility = visible;
            btn_duplicate.Visibility = visible;
        }

        #endregion
    }
}