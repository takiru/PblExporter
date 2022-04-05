using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using PblExporter.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PblExporter
{
    class NewLineConverter : ITypeConverter
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            throw new NotImplementedException();
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
            {
                return null;
            }

            var result = value.ToString().Replace(PbSupport.ObjectCommentNewLineSign, " ");
            return result;
        }
    }
}
