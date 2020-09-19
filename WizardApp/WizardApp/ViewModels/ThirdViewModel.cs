using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Regions;

namespace WizardApp.ViewModels
{
    public class ThirdViewModel : BindableBase, INavigationAware
    {
        private string _duplicateParameter;
        private string _allParameter;

        public string DuplicateParameter
        {
            get => _duplicateParameter;
            set => SetProperty(ref _duplicateParameter, value);
        }

        public string AllParameter
        {
            get => _allParameter;
            set => SetProperty(ref _allParameter, value);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //全パラメータを表示
            AllParameter = string.Join(",", navigationContext.Parameters.Select(pair => $"{pair.Key}={pair.Value}"));

            //同名のパラメータが複数登録された場合は、先頭の値が取得される
            if (navigationContext.Parameters["hoge"] is string message)
            {
                DuplicateParameter = message;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
