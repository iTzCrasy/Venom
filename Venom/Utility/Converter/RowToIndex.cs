using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Globalization;
using System.Windows.Controls;

namespace Venom.Utility.Converter
{
    public class RowToIndex : MarkupExtension, IValueConverter
    {
        private static RowToIndex _converter;


        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return value is DataGridRow row ? row.GetIndex( ) + 1 : -1;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException( );
        }

        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            if( _converter == null )
            {
                _converter = new RowToIndex( );
            }

            return _converter;
        }
    }
}
