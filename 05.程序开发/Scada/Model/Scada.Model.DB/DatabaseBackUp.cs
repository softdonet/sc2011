using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace Scada.Model.DB
{
    /// <summary>
    /// ¿‡DatabaseBackUp°£
    /// </summary>
    public class DatabaseBackUp
    {
        private Guid _guid;
        private string _databasename;
        private DateTime? _backupdate;
        private decimal? _backupsize;
        private string _backuppeople;
        private string _comment;
        /// <summary>
        /// 
        /// </summary>
        public Guid Guid
        {
            set { _guid = value; }
            get { return _guid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DatabaseName
        {
            set { _databasename = value; }
            get { return _databasename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? BackUpDate
        {
            set { _backupdate = value; }
            get { return _backupdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? BackUpSize
        {
            set { _backupsize = value; }
            get { return _backupsize; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BackUpPeople
        {
            set { _backuppeople = value; }
            get { return _backuppeople; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Comment
        {
            set { _comment = value; }
            get { return _comment; }
        }
    }
}

