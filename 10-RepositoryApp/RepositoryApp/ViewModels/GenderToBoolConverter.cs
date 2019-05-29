using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RepositoryApp.ViewModels
{
    public class GenderToBoolConverter : IValueConverter
    {
        // Enum → bool 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //バインディング ソースによって生成された値
            //→ViewModelのプロパティの読み込みや、ViewModelのプロパティが変更された時
            //valueの型はAuthorGenderプロパティをバインディングしているのでGender(enum)

            //XAMLに定義されたConverterParameterをEnumに変換する
            var parameterString = parameter as string;
            if (parameterString == null)
            {
                return DependencyProperty.UnsetValue;
            }

            var gender = (Gender)Enum.Parse(typeof(Gender), parameterString);

            //ConverterParameterから作成したEnumと、バインディングソース（AuthorGenderプロパティ）が等しいか？
            return gender.Equals(value);
        }

        // bool → Enum 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //バインディング ターゲットによって生成された値
            //→ラジオボタンが変更された時
            //valueの型はbool

            //RaddioButtonなので、value=nullで呼ばれる事は無い。
            //if (value == null)
            //{
            //    return DependencyProperty.UnsetValue;
            //}

            //RaddioButtonなので、value=falseで呼ばれる事は無い。
            //if (!(bool) value)
            //{
            //    return DependencyProperty.UnsetValue;
            //}

            //ConverterParameterからEnumを作成して返す。
            var parameterString = parameter as string;
            if (parameterString == null)
            {
                return DependencyProperty.UnsetValue;
            }

            return (Gender)Enum.Parse(typeof(Gender), parameterString);
        }
    }
}
