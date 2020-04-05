using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPFUI.CustomConverters
{
    public class FileToBitmapConverter : IValueConverter
    {
        private static readonly Dictionary<string, BitmapImage> _convertedImage =
            new Dictionary<string, BitmapImage>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string filename))
            {
                return null;
            }

            if (!_convertedImage.ContainsKey(filename))
            {
                _convertedImage.Add(filename,
                               new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}{filename}",
                                                       UriKind.Absolute)));
            }

            return _convertedImage[filename];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}