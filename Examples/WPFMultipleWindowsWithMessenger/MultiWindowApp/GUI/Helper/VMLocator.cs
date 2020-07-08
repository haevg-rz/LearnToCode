using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GUI.ViewModel;

namespace GUI.Helper
{
    public class VMLocator
    {
        public VMLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ConfigViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public ConfigViewModel Config => ServiceLocator.Current.GetInstance<ConfigViewModel>();
    }
}
