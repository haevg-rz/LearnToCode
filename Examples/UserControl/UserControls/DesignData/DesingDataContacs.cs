using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UserControl.UserControls.DesignData
{
    public class ObservableCollectionContracts : ObservableCollection<IContact>
    {
    }

    public class WrapPanelUserControlPropertiesDesignData : IWrapPanelUserControlProperties
    {
        public IEnumerable<IContact> WrapItems { get; set; }
    }
}