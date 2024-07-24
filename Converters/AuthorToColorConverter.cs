using Avalonia.Data.Converters;
using Avalonia.Media;
using StudyAssistant.Models;
using System;
using System.Globalization;

namespace StudyAssistant.Converters
{
    internal class AuthorToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is AuthorType authorType)
            {
                return authorType switch
                {
                    AuthorType.User => Brushes.Gray,
                    AuthorType.AI => new SolidColorBrush(Color.Parse("#191919")),
                    _ => Brushes.Transparent
                };
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
