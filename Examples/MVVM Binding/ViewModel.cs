using System;
using System.Collections.Generic;
using System.Text;

namespace MVVM_Binding
{
    public class ViewModel
    {
        public string Content1 { get; set; } = "Hallo";
        public string Content2 { get; set; }

        public string Content2TextBlock
        {
            get => "#: "+ Content2;
        }
    }
}
