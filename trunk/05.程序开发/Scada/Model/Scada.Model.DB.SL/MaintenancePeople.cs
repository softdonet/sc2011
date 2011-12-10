using System;
using System.Text;
namespace Scada.Model.DB.SL
{
    /// <summary>
    /// ¿‡MaintenancePeople°£
    /// </summary>
    public class MaintenancePeople
    {
        private Guid _id;
        private string _mainno;
        private string _mainname;
        private string _city;
        private string _native;
        private string _telephone;
        private string _mobileBack;
        private string _email;
        /// <summary>
        /// Guid
        /// </summary>
        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MainNo
        {
            set { _mainno = value; }
            get { return _mainno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MainName
        {
            set { _mainname = value; }
            get { return _mainname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string City
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Native
        {
            set { _native = value; }
            get { return _native; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Telephone
        {
            set { _telephone = value; }
            get { return _telephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MobileBack
        {
            set { _mobileBack = value; }
            get { return _mobileBack; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
    }
}

