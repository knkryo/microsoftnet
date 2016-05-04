using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Collections;
using Common.ClassLibrary.Utils;

namespace Common.ClassLibrary.FileIO.Reader
{
    public class CSVReader
    {
        public List<List<String>> ReadCsvToStringList(string csvpath)
        {
            try
            {
                using (System.IO.TextReader r = new System.IO.StreamReader(csvpath))
                {
                    return  Utils.ObjectConverter.CsvToArrayList(r.ReadToEnd());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public System.Data.DataTable ReadCsvToDataTable(string csvpath)
        {
            return Utils.ObjectConverter.TwoDListToDataTable(this.ReadCsvToStringList(csvpath));
        }

    }
}
