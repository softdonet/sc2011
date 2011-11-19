using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.CommClass
{
    public class ScanDatas
    {
        public List<ScanDataModel> GetScanData()
        {
            List<ScanDataModel> ListscanDataModel = new List<ScanDataModel>();
            for (int i = 0; i < 6; i++)
            {
                ScanDataModel scanDataModel = new ScanDataModel();
                scanDataModel.SEQ = 100 + i;
                scanDataModel.BODYNO = "IDF " + new Random().Next(100000, 999999);
                scanDataModel.ScanTime = DateTime.Now.AddMinutes(-i).ToString("yyyy-MM-dd HH:mm:ss");
                ListscanDataModel.Add(scanDataModel);
            }
            return ListscanDataModel;
        }
    }
}
