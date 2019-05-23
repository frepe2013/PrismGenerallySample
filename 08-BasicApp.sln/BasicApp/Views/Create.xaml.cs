using System;
using System.Windows;
using BasicApp.ViewModels;

namespace BasicApp.Views
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        public Create()
        {
            DataContextChanged += (s, a) =>
            {
                CreateViewModel vm = DataContext as CreateViewModel;
                if (vm == null) throw new InvalidCastException();
                vm.CloseDialogBox = CloseDialogBox;
            };

            InitializeComponent();
        }

        private void CloseDialogBox(bool b)
        {
            DialogResult = b;
        }
    }
}
