using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ClassLibrary.FileIO
{
    public class FormatConverter
    {

        public static void XmlDataTableToCSV(string xmlFullPath , string csvFullPath)
        {
            try
            {
                using (System.Data.DataSet ds = new System.Data.DataSet())
                {
                    ds.ReadXml(xmlFullPath);
                    using (System.Data.DataTable dt = ds.Tables[0])
                    {
                        ClassLibrary.FileIO.Writer.CSVWriter dtcsv = new ClassLibrary.FileIO.Writer.CSVWriter();
                        dtcsv.Write(dt,csvFullPath);
                        dtcsv = null;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


    }
}
