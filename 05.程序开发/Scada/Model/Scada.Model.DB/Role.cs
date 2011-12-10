using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace Scada.Model.DB
{
	/// <summary>
	/// ¿‡Role°£
	/// </summary>
	public class Role
	{
		private Guid _roleid;
		private string _rolename;
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
		public string RoleName
		{
			set{ _rolename=value;}
			get{return _rolename;}
		}
	}
}

