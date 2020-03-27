using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using WPFUiBindingPerformance.DataDefinitions;

namespace WPFUiBindingPerformance.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region ClassDataMembers

        #region Properties

        private string timeToCreateDataAndFillTheCollection;

        public string TimeToCreateDataAndFillTheCollection
        {
            get => this.timeToCreateDataAndFillTheCollection;
            set => base.Set(ref this.timeToCreateDataAndFillTheCollection, value);
        }

        private int amountOfProperties;

        public int AmountOfProperties
        {
            get
            {
                if (this.amountOfProperties != 0) return this.amountOfProperties;
                this.AmountOfProperties = 50;
                return this.amountOfProperties;
            }
            set => base.Set(ref this.amountOfProperties, Convert.ToInt32(value));
        }

        #endregion

        #region RelayCommands

        public RelayCommand LoadNestedTypeCommand { get; set; }
        public RelayCommand LoadNonNestedTypeCommand { get; set; }
        public RelayCommand LoadSilentCollectionCommand { get; set; }
        public RelayCommand ClosingCommand { get; set; }
        public RelayCommand ClearAllDataCommand { get; set; }

        #endregion

        #region Collections

        public ObservableCollection<TypeWithNestedProperties> NestedList { get; set; }
        public ObservableCollection<TypeWithoutNestedProperties> NonNestedList { get; set; }
        public SilentObservableCollection<TypeWithoutNestedProperties> SilentList { get; set; }

        #endregion

        #endregion

        public MainViewModel()
        {
            this.LoadNestedTypeCommand = new RelayCommand(this.LoadNestedTypeCommandHandling);
            this.LoadNonNestedTypeCommand = new RelayCommand(this.LoadNonNestedTypeCommandHandling);
            this.LoadSilentCollectionCommand = new RelayCommand(this.LoadSilentCollectionCommandHandling);
            this.ClosingCommand = new RelayCommand(this.ClosingCommandHandling);
            this.ClearAllDataCommand = new RelayCommand(this.ClearAllDataCommandHandling);

            this.NestedList = new ObservableCollection<TypeWithNestedProperties>();
            this.NonNestedList = new ObservableCollection<TypeWithoutNestedProperties>();
            this.SilentList = new SilentObservableCollection<TypeWithoutNestedProperties>();
        }

        #region ClassFunctionMembers

        #region CommandHandlings

        private void ClearAllDataCommandHandling()
        {
            this.SilentList.Clear();
            this.NestedList.Clear();
            this.NonNestedList.Clear();
        }

        private void ClosingCommandHandling()
        {
            ViewModelLocator.Cleanup();
        }

        private void LoadSilentCollectionCommandHandling()
        {
            var twonp = new TypeWithoutNestedProperties();

            var sw = Stopwatch.StartNew();
            this.SilentList.AddRange(twonp.GetTypeWithoutNestedPropertieses("TypeName", this.AmountOfProperties * 3));
            this.TimeToCreateDataAndFillTheCollection = (sw.ElapsedMilliseconds / 1000.0).ToString();
        }

        private void LoadNonNestedTypeCommandHandling()
        {
            var twonp = new TypeWithoutNestedProperties();

            var sw = Stopwatch.StartNew();
            foreach (var item in twonp.GetTypeWithoutNestedPropertieses("TypeName", this.AmountOfProperties * 3))
            {
                this.NonNestedList.Add(item);
            }
            this.TimeToCreateDataAndFillTheCollection = (sw.ElapsedMilliseconds / 1000.0).ToString();
        }

        private void LoadNestedTypeCommandHandling()
        {
            var sw = Stopwatch.StartNew();
            this.NestedList.Add(new TypeWithNestedProperties("Type1", this.AmountOfProperties));
            this.NestedList.Add(new TypeWithNestedProperties("Type2", this.AmountOfProperties));
            this.NestedList.Add(new TypeWithNestedProperties("Type3", this.AmountOfProperties));
            this.TimeToCreateDataAndFillTheCollection = (sw.ElapsedMilliseconds / 1000.0).ToString();
        }

        #endregion

        #endregion
    }
}