﻿namespace Binding
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class ObservableObject : INotifyPropertyChanged
    {
        public ObservableObject()
        {
            Properties = new Dictionary<string, PropertyInfo>();
        }

        public Dictionary<string, PropertyInfo> Properties { get; private set; }
        
        public void RegisterProperty<T>(string name, Func<T> getter)
        {
            var property = new PropertyInfo<T>(name, getter);

            Properties.Add(name, property);
        }

        public void RegisterProperty<T>(string name, Action<T> setter)
        {
            var notifyingSetter = MakeNotifyingSetter(name, setter);
            var property = new PropertyInfo<T>(name, notifyingSetter);

            Properties.Add(name, property);
        }

        public void RegisterProperty<T>(string name, Func<T> getter, Action<T> setter)
        {
            var notifyingSetter = MakeNotifyingSetter(name, setter);
            var property = new PropertyInfo<T>(name, getter, notifyingSetter);

            Properties.Add(name, property);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Action<PropertyInfo<T>, T> MakeNotifyingSetter<T>(string name, Action<T> setter)
        {
            return (PropertyInfo<T> property, T value) =>
            {
                if (value.Equals(property.Get()))
                {
                    return;
                }

                setter(value);
                RaisePropertyChangedEvent(name);
            };
        }
    }
}