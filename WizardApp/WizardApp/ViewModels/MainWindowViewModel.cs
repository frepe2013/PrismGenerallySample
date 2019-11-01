using System.Linq;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using WizardApp.Views;

namespace WizardApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private string _title = "Prism Application";
        private bool _canNextButton = true;
        private bool _canPrevButton;
        private bool _canFinishButton;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool CanPrevButton
        {
            get { return _canPrevButton; }
            set { SetProperty(ref _canPrevButton, value); }
        }

        public bool CanNextButton
        {
            get { return _canNextButton; }
            set { SetProperty(ref _canNextButton, value); }
        }

        public bool CanFinishButton
        {
            get { return _canFinishButton; }
            set { SetProperty(ref _canFinishButton, value); }
        }

        public DelegateCommand PrevCommand { get; set; }

        public DelegateCommand NextCommand { get; set; }

        public DelegateCommand FinishCommand { get; set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            PrevCommand = new DelegateCommand(PrevNavigate).ObservesCanExecute(() => CanPrevButton);
            NextCommand = new DelegateCommand(NextNavigate).ObservesCanExecute(() => CanNextButton);
            FinishCommand = new DelegateCommand(Finish).ObservesCanExecute(() => CanFinishButton);
        }

        private void PrevNavigate()
        {
            var type = _regionManager.Regions["ContentRegion"].ActiveViews.First().GetType();

            var navigationPath = "";
            if (type == typeof(Second))
            {
                navigationPath = nameof(First);
                CanPrevButton = false;
            }
            else if (type == typeof(Third))
            {
                navigationPath = nameof(Second);
                CanNextButton = true;
                CanFinishButton = false;
            }

            _regionManager.RequestNavigate("ContentRegion", navigationPath);
        }
        private void NextNavigate()
        {
            var type = _regionManager.Regions["ContentRegion"].ActiveViews.First().GetType();

            var navigationPath = "";
            if (type == typeof(First))
            {
                navigationPath = nameof(Second);
                CanPrevButton = true;
            }
            else if (type == typeof(Second))
            {
                navigationPath = nameof(Third);
                CanNextButton = false;
                CanFinishButton = true;
            }

            _regionManager.RequestNavigate("ContentRegion", navigationPath);
        }

        private void Finish()
        {
            Application.Current.MainWindow?.Close();
        }
    }
}
