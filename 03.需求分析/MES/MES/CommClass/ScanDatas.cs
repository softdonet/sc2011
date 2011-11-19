using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.CommClass
{
   public class ScanDatas
    {
       public class ScanModel
       {
           public int SEQ { get; set; }
           public string BODYNO { get; set; }
           public string ScanTime { get; set; }
       }
       public List<ScanModel> GetScanData()
       {
           
           List<ScanModel> ListScanMode = new List<ScanModel>();
           for (int i = 0; i < 6; i++)
           {
               ScanModel scanModel = new ScanModel();
               scanModel.SEQ = 100 + i;
               scanModel.BODYNO = "IDF " + new Random().Next(100000, 999999);
               scanModel.ScanTime = DateTime.Now.AddMinutes(-i).ToString("yyyy-MM-dd HH:mm:ss");
               ListScanMode.Add(scanModel);
           }
           return ListScanMode;
       }
    }
}
