using System;
using System.Text;
using System.Collections.Generic;
using Scada.BLL.Contract;
using Scada.Utility.Common.Transfer;
using Scada.Model.Entity;




namespace Scada.BLL.Implement
{

    public class SystemManagerService : ISystemManagerService
    {


        public int AddDD(int x, int y)
        {
            return x + y;
        }

        public string ListStudents()
        {
            List<Student> studens = new List<Student>();
            studens.Add(new Student { Name = "张三", Age = 17 });
            studens.Add(new Student { Name = "李四", Age = 19 });
            return BinaryObjTransfer.JsonSerializer<List<Student>>(studens);
        }

    }
}
