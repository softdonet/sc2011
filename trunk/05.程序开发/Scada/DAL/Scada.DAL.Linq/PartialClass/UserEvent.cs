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
            get
            {
                string result = UserEventDealDetails.OrderBy(e => e.StepNo).Select(e => e.StepName + "=>").ToString();
                return "报告维护人员==》到达==》";
            }
        }
        public string Step1
        {
            get
            {
                var obj = this.UserEventDealDetails.Where(e => e.StepNo == 1);
                if (obj.Any())
                {
                    return obj.First().StepName;
                }
                return string.Empty;
            }
        }

        public string Step2
        {
            get
            {
                var obj = this.UserEventDealDetails.Where(e => e.StepNo ==2);
                if (obj.Any())
                {
                    return obj.First().StepName;
                }
                return string.Empty;
            }
        }


        public string Step3
        {
            get
            {
                var obj = this.UserEventDealDetails.Where(e => e.StepNo ==3);
                if (obj.Any())
                {
                    return obj.First().StepName;
                }
                return string.Empty;
            }
        }

        public string Step4
        {
            get
            {
                var obj = this.UserEventDealDetails.Where(e => e.StepNo == 4);
                if (obj.Any())
                {
                    return obj.First().StepName;
                }
                return string.Empty;
            }
        }

        public string Step5
        {
            get
            {
                var obj = this.UserEventDealDetails.Where(e => e.StepNo ==5);
                if (obj.Any())
                {
                    return obj.First().StepName;
                }
                return string.Empty;
            }
        }
    }
}
