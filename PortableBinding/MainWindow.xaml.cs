// -----------------------------------------------------------------------
//  <copyright file="MainWindow.xaml.cs" company="Ron Parker">
//   Copyright 2014 Ron Parker
//  </copyright>
//  <summary>
//   Implements the main window class.
//  </summary>
// -----------------------------------------------------------------------

namespace PortableBinding
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using Binding;
    using ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// <para>Unlike most WPF applications, this one implements binding using a portable binding mechanism.
    /// Therefore, the properties and there bindings are declared in this file.</para>
    /// </summary>
    public partial class MainWindow : Window, INotifyingObject
    {
        /// <summary>
        /// The View Model displayed by this View.
        /// </summary>
        private readonly ViewModel _viewModel = new ViewModel();

        /// <summary>
        /// The object used for binding properties in this View.
        /// </summary>
        private readonly BindingObject _binding;

        /// <summary>
        /// Initializes static members of the <see cref="MainWindow"/> class.
        /// </summary>
        static MainWindow()
        {
            AddTextBoxProperties("NumericTextBox", (MainWindow o) => { return o.NumericTextBox; });
            AddTextBoxProperties("StringTextBox", (MainWindow o) => { return o.StringTextBox; });
            AddTextBoxProperties("ComputedTextBox", (MainWindow o) => { return o.ComputedTextBox; });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            _binding = new BindingObject(_viewModel);

            InitializeBindings();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Invoked whenever the effective value of any dependency property on this
        /// <see cref="T:System.Windows.FrameworkElement" /> has been updated. The specific dependency
        /// property that changed is reported in the arguments parameter. Overrides
        /// <see cref="M:System.Windows.DependencyObject.OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs)" />.
        /// </summary>
        ///
        /// <remarks>   Ron, 12/24/2014. </remarks>
        ///
        /// <param name="e">    The event data that describes the property that changed, as well as old
        ///                     and new values. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            // Propogate the notification for INotifyingObject.
            OnPropertyChangedEvent(e.Property.Name);
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        public void OnPropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Adds the properties for a specific TextBox within the View.
        /// </summary>
        /// <param name="name">The name of the TextBox.</param>
        /// <param name="textBoxGetter">The function for getting the specific TextBox for given window.</param>
        private static void AddTextBoxProperties(string name, Func<MainWindow, TextBox> textBoxGetter)
        {
            Property.Register(name, (MainWindow o) => { return textBoxGetter(o); });

        }

        /// <summary>
        /// Initializes the bindings.
        /// </summary>
        private void InitializeBindings()
        {
            BindTextBox("NumericTextBox", "Number");

            BindTextBox("StringTextBox", "Text");
            BindTextBox("ComputedTextBox", "Computed");
        }

        /// <summary>
        /// Binds a TextBox to a property in the View Model.
        /// </summary>
        /// <param name="target">The name of the target TextBox.</param>
        /// <param name="source">The name of the source View Model property.</param>
        private void BindTextBox(string target, string source)
        {
            string textProperty = target + ".Text";

            // Bind the View to the View Model
            _binding.Bind(this, textProperty, source);
            _binding.Bind(this, target + ".IsEnabled", source + ".CanWrite");

            var control = (TextBox)Property.Find(GetType(), target).Get(this);
            control.TextChanged += (object sender, TextChangedEventArgs e) => OnPropertyChangedEvent(textProperty);
        }
    }
}