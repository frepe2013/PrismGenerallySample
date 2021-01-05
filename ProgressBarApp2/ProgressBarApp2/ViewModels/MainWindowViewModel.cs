using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;

namespace ProgressBarApp2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private int _progressValue1;
        private int _progressValue2;
        private int _progressValue3;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public int ProgressValue1
        {
            get => _progressValue1;
            set => SetProperty(ref _progressValue1, value);
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

        public List<PValue> ProgressValues { get; set; }

        public DelegateCommand LoadedCommand { get; set; }

        //public DelegateCommand ContentRenderedCommand { get; set; }

        public MainWindowViewModel()
        {
            ProgressValues = new List<PValue>
            {
                new PValue(),
                new PValue(),
                new PValue(),
            };

            LoadedCommand = new DelegateCommand(ExecuteMethodRun);
            //LoadedCommand = new DelegateCommand(ExecuteMethod);
            //ContentRenderedCommand = new DelegateCommand(ExecuteMethod);
        }

        private void ExecuteMethodRun()
        {
            Task.Run(() =>
            {
                foreach (var t in ProgressValues)
                {
                    NewMethod5(t);
                }
            });
        }

        private static void NewMethod5(PValue t)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                t.ProgressValue += 10;
            }
        }

        private async void ExecuteMethod()
        {
            foreach (var t in ProgressValues)
            {
                await NewMethod4(t);
            }
            //await NewMethod1();

            //await NewMethod2();

            //await NewMethod3();
        }

        private static async Task NewMethod4(PValue t)
        {
            for (var j = 0; j < 10; j++)
            {
                await Task.Delay(500);
                t.ProgressValue += 10;
            }
        }

        private async Task NewMethod3()
        {
            for (var i = 0; i < 10; i++)
            {
                await Task.Delay(500);
                ProgressValue3 += 10;
            }
        }

        private async Task NewMethod2()
        {
            for (var i = 0; i < 10; i++)
            {
                await Task.Delay(500);
                ProgressValue2 += 10;
            }
        }

        private async Task NewMethod1()
        {
            for (var i = 0; i < 10; i++)
            {
                await Task.Delay(500);
                ProgressValue1 += 10;
            }
        }
    }
}
