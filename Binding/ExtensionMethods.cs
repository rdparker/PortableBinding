// -----------------------------------------------------------------------
//  <copyright file="ExtensionMethods.cs" company="Ron Parker">
//   Copyright 2014 Ron Parker
//  </copyright>
//  <summary>
//   Implements extension methods for portable binding.
//  </summary>
// -----------------------------------------------------------------------

namespace Binding
{
    using System.ComponentModel;

    /// ////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Extension methods for portable binding. </summary>
    ///
    /// <remarks>   Last edited by Ron, 1/4/2015. </remarks>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////
    public static class ExtensionMethods
    {
        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An INotifyPropertyChanged extension method that raises the given PropertyChangedEvent.
        /// Calling this method instead of implementing the logic directly avoids having to test the
        /// PropertyChangedEventHandler at each call site, while being careful to avoid a race condition.
        /// </summary>
        ///
        /// <remarks>   Last edited by Ron, 1/4/2015. </remarks>
        ///
        /// <param name="obj">          The object to act on. </param>
        /// <param name="handler">      The event handler. </param>
        /// <param name="propertyName"> The name of the property. </param>
        ///
        /// <example>
        /// Instead of writing this:
        /// <code>
        /// public void OnPropertyChangedEvent(string propertyName)
        /// {
        ///     var handler = PropertyChanged;
        /// 
        ///     if (handler != null)
        ///         handler(new PropertyChangedEventArgs(propertyName);
        /// 
        /// }
        /// </code>
        /// the user can simply write
        /// <code>
        /// public void OnPropertyChangedEvent(string propertyName)
        /// {
        ///     this.RaiseEvent(PropertyChanged, propertyName);
        /// }
        /// </code>
        /// </example>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void RaiseEvent(
            this INotifyPropertyChanged obj, PropertyChangedEventHandler handler, string propertyName)
        {
            if (handler != null)
            {
                handler(obj, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}