using GalaSoft.MvvmLight.Ioc;

namespace UserControl
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<ViewModel>();
        }

        public ViewModel ViewModel => SimpleIoc.Default.GetInstance<ViewModel>();
    }
}