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
    using RabidWarren.Binding;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows.Controls;
    using ViewModel;
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// <para>Unlike most WPF applications, this one implements binding using a portable binding mechanism.
    /// Therefore, the properties and there bindings are declared in this file.</para>
    /// </summary>
    public partial class MainWindow : System.Windows.Window, INotifyingObject
    {
        /// <summary>
        /// The View Model displayed by this View.
        /// </summary>
        readonly ViewModel _viewModel = new ViewModel();

        /// <summary>
        /// The object used for binding properties in this View.
        /// </summary>
        readonly BindingObject _binding;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            _binding = new BindingObject(_viewModel);

            InitializeBindings();
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Raises the property changed event. </summary>
        ///
        /// <remarks>   Last edited by Ron, 1/4/2015. </remarks>
        ///
        /// <param name="propertyName"> The name of the property that changed. </param>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        public void OnPropertyChangedEvent(string propertyName)
        {
            var notify = this.PropertyChanged;

            if (notify != null)
                notify(this, new PropertyChangedEventArgs(propertyName));
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Invoked whenever the effective value of any dependency property on this
        /// <see cref="T:System.Windows.FrameworkElement" /> has been updated. The specific dependency
        /// property that changed is reported in the arguments parameter. Overrides
        /// <see cref="M:System.Windows.DependencyObject.OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs)" />.
        /// </summary>
        ///
        /// <remarks>   Last edited by Ron, 12/24/2014. </remarks>
        ///
        /// <param name="e">    The event data that describes the property that changed, as well as old
        ///                     and new values. </param>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        protected override void OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            // Propogate the notification for INotifyingObject.
            var notify = this.PropertyChanged;

            if (notify != null)
                notify(this, new PropertyChangedEventArgs(e.Property.Name));
        }

        /// <summary>
        /// Initializes the bindings.
        /// </summary>
        void InitializeBindings()
        {
            Bind(mw => mw.NumericTextBox, vm => vm.Number);
            Bind(mw => mw.StringTextBox, vm => vm.Text);
            Bind(mw => mw.ComputedTextBox, vm => vm.Computed);
        }

        void Bind<T>(Expression<Func<MainWindow, TextBox>> target, Expression<Func<ViewModel, T>> source)
        {
            var textBox = target.Compile().Invoke(this);
            
            _binding.Bind(this, target.Compose(tb => tb.Text), _viewModel, source);
            _binding.Bind(this, target.Compose(tb => tb.IsEnabled), _viewModel, source.Compose(s => s.CanWrite()));

            textBox.TextChanged +=
                (object sender, TextChangedEventArgs e) => OnPropertyChangedEvent(textBox.Name + ".Text");
        }
    }
}