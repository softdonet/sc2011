using System;




namespace Scada.Client.SL.Modules.DiagramAnalysis
{


    /// <summary>
    /// +=<>翻页
    /// </summary>
    public class DeviceComaprePageTurn
    {

        #region 开放属性

        private DateTime _starDate;
        public DateTime StarDate
        {
            get { return this._starDate; }
            set { this._starDate = value; }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return this._endDate; }
            set { this._endDate = value; }
        }

        private DateSelMode _compareType;
        public DateSelMode ComparType
        {
            get { return this._compareType; }
            set { this._compareType = value; }
        }

        #endregion


        #region 变量声明

        private Int32 _intervalNum = 0;

        #endregion


        #region 静态构造

        private static DeviceComaprePageTurn _devicePageTurn;

        public static DeviceComaprePageTurn getInstance()
        {
            if (_devicePageTurn == null)
            {
                _devicePageTurn = new DeviceComaprePageTurn();
            }
            return _devicePageTurn;
        }


        #endregion


        #region 构造函数

        public DeviceComaprePageTurn()
        {

        }

        #endregion


        #region 开放方法

        //付初始值
        public void AddPrepaid(DateTime start, DateTime end, DateSelMode compareType)
        {

            this._starDate = start;
            this._endDate = end;
            this._compareType = compareType;

            if (_compareType == DateSelMode.天)
            {
                TimeSpan ts = end - start;
                this._intervalNum = (Int32)ts.TotalDays;
            }
            else if (_compareType == DateSelMode.月)
                this._intervalNum = (Int32)GetMonthSpan(start, end);

        }


        //  +加周期
        public void Add()
        {
            this._intervalNum++;
            if (this._compareType == DateSelMode.天)
                this._endDate = this._endDate.AddDays(1);
            else if (this._compareType == DateSelMode.月)
                this._endDate = this._endDate.AddMonths(1);
        }

        //  -减周期
        public void Remove()
        {
            if (this._intervalNum == 1)
                throw new Exception("周期最小粒度为1");
            this._intervalNum--;
            if (this._compareType == DateSelMode.天)
                this._endDate = this._endDate.AddDays(-1);
            else if (this._compareType == DateSelMode.月)
                this._endDate = this._endDate.AddMonths(-1);

        }

        //  >向前翻
        public void Forth()
        {
            if (this._compareType == DateSelMode.天)
            {
                this._starDate = this._starDate.AddDays(-this._intervalNum);
                this._endDate = this._endDate.AddDays(-this._intervalNum);
            }
            else if (this._compareType == DateSelMode.月)
            {
                this._starDate = this._starDate.AddMonths(-this._intervalNum);
                this._endDate = this._endDate.AddMonths(-this._intervalNum);
            }
        }

        //  <向后翻
        public void Back()
        {
            if (this._compareType == DateSelMode.天)
            {
                this._starDate = this._starDate.AddDays(this._intervalNum);
                this._endDate = this._endDate.AddDays(this._intervalNum);
            }
            else if (this._compareType == DateSelMode.月)
            {
                this._starDate = this._starDate.AddMonths(this._intervalNum);
                this._endDate = this._endDate.AddMonths(this._intervalNum);
            }
        }

        #endregion


        #region 私有方法

        private double GetMonthSpan(DateTime fBeginDateTime, DateTime fEndDateTime)
        {

            if (fBeginDateTime > fEndDateTime)
            {
                throw new Exception("开始时间应小于或等结束时间");
            }

            // 计算整年的情况
            int prefullYear = fEndDateTime.Year - fBeginDateTime.Year;
            int fullYear = (fBeginDateTime.AddYears(prefullYear) > fEndDateTime)
                ? prefullYear - 1 : prefullYear;
            int fullMonth = fullYear * 12;
            DateTime curBeginDate = fBeginDateTime.AddMonths(fullMonth);

            while (curBeginDate < fEndDateTime)
            {
                DateTime curEndDate = curBeginDate.AddMonths(1);
                if (curEndDate > fEndDateTime)
                {
                    double days = (fEndDateTime - curBeginDate).TotalDays;
                    double fullDaysOfMonth = (curBeginDate.AddMonths(1) - curBeginDate).TotalDays;
                    double p = days / fullDaysOfMonth;
                    return fullMonth + p;
                }
                else
                {
                    curBeginDate = curEndDate;
                    fullMonth++;
                }
            }

            return fullMonth;
        }

        #endregion


    }

}
