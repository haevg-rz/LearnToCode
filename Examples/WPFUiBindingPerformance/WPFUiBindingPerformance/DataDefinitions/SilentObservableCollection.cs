using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WPFUiBindingPerformance.DataDefinitions
{
    public class SilentObservableCollection<T> : ObservableCollection<T>
    {
        private bool suppressNotification = false;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!this.suppressNotification)
                base.OnCollectionChanged(e);
        }

        public void AddRange(IEnumerable<T> list)
        {
            if (list == null)
                throw new ArgumentNullException($"Empty Lists are not supported:({nameof(list)})");

            this.suppressNotification = true;

            foreach (var item in list)
            {
                this.Add(item);
            }

            this.suppressNotification = false;
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}