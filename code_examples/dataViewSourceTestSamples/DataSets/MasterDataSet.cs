using System;
namespace dataViewSourceTestSamples.DataSets {
    
    
    public partial class MasterDataSet {
    }
}

namespace dataViewSourceTestSamples.DataSets.MasterDataSetTableAdapters
{
    public partial class ZIPCODETableAdapter {

        System.Data.SqlClient.SqlCommand cmd;
        System.Data.SqlClient.SqlDataAdapter _adpt;
        public void FillOfPrefecture(DataSets.MasterDataSet.ZIPCODEDataTable dataTable, string prefectureCode, bool isMerge)
        {
            string query = string.Empty;
            if (cmd == null){cmd = this.Connection.CreateCommand();}
            if (_adpt == null) { _adpt = new System.Data.SqlClient.SqlDataAdapter(cmd);}
            query = "SELECT * FROM ZIPCODE ";
            query += "WHERE LEFT(XJIS_CITY_CODE,2) = @PREF_CODE";
            cmd.Parameters.Clear(); 
            cmd.CommandText = query;
            cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("PREF_CODE",prefectureCode.PadLeft(2,Convert.ToChar("0")).ToString()));

            if (isMerge == true)
            {
                System.Data.DataTable dt = new DataSets.MasterDataSet.ZIPCODEDataTable();
                _adpt.Fill(dt);
                dataTable.Merge(dt);
            }
            else
            {
                dataTable.Clear();
                _adapter.Fill(dataTable);
            }
        }
    }
}
