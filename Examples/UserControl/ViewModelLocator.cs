using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;

namespace UserControl
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
        }

        public ViewModel ViewModel
        {
            get { return new ViewModel(); }
        }
    }
}