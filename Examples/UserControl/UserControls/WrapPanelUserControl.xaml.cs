using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserControl.UserControls
{
    /// <summary>
    /// Interaktionslogik für WrapPanelUserControl.xaml
    /// </summary>
    public partial class WrapPanelUserControl : System.Windows.Controls.UserControl, IWrapPanelUserControlProperties
    {
        public WrapPanelUserControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty WrapItemsProperty = DependencyProperty.Register(
            "WrapItems", typeof(IEnumerable<IContact>), typeof(WrapPanelUserControl), new PropertyMetadata(default(IEnumerable<IContact>)));

        public IEnumerable<IContact> WrapItems
        {
            get { return (IEnumerable<IContact>) GetValue(WrapItemsProperty); }
            set { SetValue(WrapItemsProperty, value); }
        }
    }

    public interface IWrapPanelUserControlProperties
    {
        IEnumerable<IContact> WrapItems { get; set; }
    }
}
