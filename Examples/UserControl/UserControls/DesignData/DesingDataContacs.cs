using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Text;

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