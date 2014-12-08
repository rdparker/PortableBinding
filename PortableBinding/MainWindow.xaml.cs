using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PortableBinding
{
    using Binding;
    using ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModel _viewModel = new ViewModel();
        private readonly BindingObject _binding;

        public MainWindow()
        {
            InitializeComponent();

            _binding = new BindingObject(_viewModel);
            
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            _binding.RegisterProperty("NumericTextBox_Text", () => { return NumericTextBox.Text; }, (x) => NumericTextBox.Text = x);
            _binding.RegisterProperty("StringTextBox_Text", () => { return StringTextBox.Text; }, (x) => StringTextBox.Text = x);
            _binding.RegisterProperty("ComputedTextBox_Text", () => { return ComputedTextBox.Text; }, (x) => ComputedTextBox.Text = x);

            _binding.Bind("Number", "NumericTextBox_Text");
            _binding.Bind("Text", "StringTextBox_Text");
            _binding.Bind("Computed", "ComputedTextBox_Text");
        }
    }
}
