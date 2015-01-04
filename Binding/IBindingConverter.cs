// -----------------------------------------------------------------------
//  <copyright file="IBindingConverter.cs" company="Ron Parker">
//   Copyright 2014 Ron Parker
//  </copyright>
//  <summary>
//   Declares the interface for binding converter classes.
//  </summary>
// -----------------------------------------------------------------------

namespace Binding
{
    using System;

    /// ////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Declares the interface binding converter classes must implement.  They must also have the
    /// <see cref="BindingConverterAttribute"/>.  If they do not
    /// <see cref="ConverterRegistry.RegisterAll()"/> will not find them.
    /// </summary>
    ///
    /// <remarks>
    /// Instead of directly inheriting this interface, classes should be derived from
    /// <see cref="BindingConverter"/>.  It provides <see cref="BindingConverter.NoValue"/>, which is
    /// used to indicate that a property could not be converted between the two types.  This can
    /// happen for example, when the user clears a numeric field that is bound to an integer before
    /// the enter the desired value.
    /// </remarks>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////
    public interface IBindingConverter
    {
        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Converts to the target type from the source type specified by the class
        /// <see cref="BindingConverterAttribute"/>.
        /// </summary>
        ///
        /// <param name="value">        The value to convert. </param>
        /// <param name="targetType">   The target conversion type. </param>
        /// <param name="parameter">    A user-supplied parameter. </param>
        ///
        /// <returns>   The converted value. </returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        object ConvertTo(object value, Type targetType, object parameter);

        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Converts from the target type to the source type specified by the class
        /// <see cref="BindingConverterAttribute"/>.
        /// </summary>
        ///
        /// <param name="value">        The value to convert. </param>
        /// <param name="targetType">   The target conversion type. </param>
        /// <param name="parameter">    A user-supplied parameter. </param>
        ///
        /// <returns>   The converted value. </returns>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        object ConvertFrom(object value, Type targetType, object parameter);
    }
}
