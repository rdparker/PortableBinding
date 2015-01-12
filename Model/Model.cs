// -----------------------------------------------------------------------
//  <copyright file="Model.cs" company="Ron Parker">
//   Copyright 2014 Ron Parker
//  </copyright>
//  <summary>
//   Represents the Model for the PortableBinding application.
//  </summary>
// -----------------------------------------------------------------------

namespace Model
{
    /// <summary>
    /// Represents the Model for the PortableBinding application.
    /// <para>This is a simple model with only one numeric and one string value.</para>
    /// </summary>
    public class Model
    {
        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
    }
}