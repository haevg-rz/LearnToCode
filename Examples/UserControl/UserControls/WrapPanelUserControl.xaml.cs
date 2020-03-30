using System.Collections.Generic;
using System.Windows;

namespace UserControl.UserControls
{
    /// <summary>
    ///     Interaktionslogik für WrapPanelUserControl.xaml
    /// </summary>
    public partial class WrapPanelUserControl : System.Windows.Controls.UserControl, IWrapPanelUserControlProperties
    {
        public static readonly DependencyProperty WrapItemsProperty = DependencyProperty.Register(
            "WrapItems", typeof(IEnumerable<IContact>), typeof(WrapPanelUserControl), new PropertyMetadata(default(IEnumerable<IContact>)));

        public WrapPanelUserControl()
        {
            this.InitializeComponent();
        }

        public IEnumerable<IContact> WrapItems
        {
            get => (IEnumerable<IContact>) this.GetValue(WrapItemsProperty);
            set => this.SetValue(WrapItemsProperty, value);
        }
    }

    public interface IWrapPanelUserControlProperties
    {
        IEnumerable<IContact> WrapItems { get; set; }
    }
}