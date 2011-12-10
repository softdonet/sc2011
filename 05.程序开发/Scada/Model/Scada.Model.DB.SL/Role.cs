using System;
using System.Text;
namespace Scada.Model.DB.SL
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

