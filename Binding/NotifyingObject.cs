// -----------------------------------------------------------------------
//  <copyright file="NotifyingObject.cs" company="Ron Parker">
//   Copyright 2014, 2015 Ron Parker
//  </copyright>
//  <summary>
//   Provides the base implementation for raising the PropertyChanged
//   event.
//  </summary>
// -----------------------------------------------------------------------

namespace Binding
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Provides the base implementation for raising the PropertyChanged event on objects which have properties that
    /// act as binding sources.
    /// </summary>
    public abstract class NotifyingObject : INotifyingObject
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">The name of the property that was changed.</param>
        public void OnPropertyChangedEvent(string propertyName)
        {
            this.RaiseEvent(PropertyChanged, propertyName);
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <typeparam name="T">The property's type.</typeparam>
        /// <param name="expression">A lambda expression that evaluates to the property.</param>
        public void OnPropertyChangedEvent<T>(Expression<Func<T>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");

            var body = expression.Body as MemberExpression;
            if (body == null) throw new ArgumentException("Invalid argument", "expression");

            var property = body.Member as PropertyInfo;
            if (property == null) throw new ArgumentException("Argument is not a property", "expression");

            this.RaiseEvent(PropertyChanged, property.Name);
        }
    }
}
