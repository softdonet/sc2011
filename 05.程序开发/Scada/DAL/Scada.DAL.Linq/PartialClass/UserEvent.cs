using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scada.DAL.Linq
{
    public partial class UserEvent
    {
        public string Memo
        {
            get {
                string result = UserEventDealDetails.OrderBy(e => e.StepNo).Select(e => e.StepName + "=>").ToString();
                //result=result.Remove(

                //return this.UserEventDealDetails
                return "报告维护人员==》到达==》";
              }
        }
        public string Step1
        {
            get
            {
                var obj1 = UserEventDealDetails.SingleOrDefault(e => e.StepNo == 1).ToString();
                if (obj1!=null)
                {
                    return obj1;
                }
                return null;
            }
        }

        public string Step2
        {
            get
            {
                var obj2 = UserEventDealDetails.SingleOrDefault(e => e.StepNo == 1).ToString();
                if (obj2 != null)
                {
                    return obj2;
                }
                return null;
            }
        }

        public string Step3
        {
            get
            {
                var obj3 = UserEventDealDetails.SingleOrDefault(e => e.StepNo == 1).ToString();
                if (obj3 != null)
                {
                    return obj3;
                }
                return null;
            }
        }

        public string Step4
        {
            get
            {
                var obj4 = UserEventDealDetails.SingleOrDefault(e => e.StepNo == 1).ToString();
                if (obj4 != null)
                {
                    return obj4;
                }
                return null;
            }
        }

        public string Step5
        {
            get
            {
                var obj5 = UserEventDealDetails.SingleOrDefault(e => e.StepNo == 1).ToString();
                if (obj5 != null)
                {
                    return obj5;
                }
                return null;
            }
        }




    }
}
