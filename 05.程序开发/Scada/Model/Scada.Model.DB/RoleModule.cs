using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace Scada.Model.DB
{
	/// <summary>
	/// ¿‡RoleModule°£
	/// </summary>
	public class RoleModule
	{
		private Guid _id;
		private Guid _roleid;
		private Guid _moduleid;
		/// <summary>
		/// 
		/// </summary>
		public Guid ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid RoleID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid ModuleID
		{
			set{ _moduleid=value;}
			get{return _moduleid;}
		}
	}
}

