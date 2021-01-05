using System.Threading;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;

namespace ProgressBarApp1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private int _progressValue;
        private int _progressValue2;
        private int _progressValue3;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        public int ProgressValue2
        {
            get => _progressValue2;
            set => SetProperty(ref _progressValue2, value);
        }

        public int ProgressValue3
        {
            get => _progressValue3;
            set => SetProperty(ref _progressValue3, value);
        }

        public DelegateCommand RunCommand { get; set; }

        public DelegateCommand RunCommand2 { get; set; }

        public DelegateCommand RunCommand3 { get; set; }

        public MainWindowViewModel()
        {
            RunCommand = new DelegateCommand(ExecuteMethod);
            RunCommand2 = new DelegateCommand(ExecuteMethod2);
            RunCommand3 = new DelegateCommand(ExecuteMethod3);
        }

        private void ExecuteMethod()
        {
            Task.Run(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    Thread.Sleep(500);
                    ProgressValue += 10;
                }
            });
        }

        private async void ExecuteMethod2()
        {
            for (var i = 0; i < 10; i++)
            {
                await Task.Delay(500);
                ProgressValue2 += 10;
            }
        }

        /// <summary>
        /// 正常に動作しないバターン
        /// </summary>
        private void ExecuteMethod3()
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    Thread.Sleep(500);
            //    ProgressValue3 += 10;
            //}
            Thread.Sleep(500);
            ProgressValue3 = 10;
            Thread.Sleep(500);
            ProgressValue3 = 20;
            Thread.Sleep(500);
            ProgressValue3 = 30;
            Thread.Sleep(500);
            ProgressValue3 = 40;
            Thread.Sleep(500);
            ProgressValue3 = 50;
            Thread.Sleep(500);
            ProgressValue3 = 60;
            Thread.Sleep(500);
            ProgressValue3 = 70;
            Thread.Sleep(500);
            ProgressValue3 = 80;
            Thread.Sleep(500);
            ProgressValue3 = 90;
            Thread.Sleep(500);
            ProgressValue3 = 100;
        }
    }
}
