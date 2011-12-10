using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using Scada.Client.SL.Controls;


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


using Scada.Model.DB.SL;




namespace Scada.Client.SL
{


    public partial class LoginPage : UserControl
    {
        Login login;
        public LoginPage()
        {
            InitializeComponent();
            login = new Login();
            this.MasterContainer.Child = login;
            login.myKeyDownEvent += new RoutedEventHandler(login_myKeyDowmEvent);
        }

        void login_myKeyDowmEvent(object sender, RoutedEventArgs e)
        {

            //add by zgj test code
            SystemManagerService.SystemManagerServiceSoapClient client =
                                    new SystemManagerService.SystemManagerServiceSoapClient();
            client.ListStudentsCompleted += new EventHandler<SystemManagerService.ListStudentsCompletedEventArgs>(testObject);
            client.ListStudentsAsync();

            if (login.txbName.Text == "admin" && login.txtPassWord.Password == "admin")
            {
                this.MasterContainer.Child = new MainPage();
            }
            else
            {
                MessageBox.Show("用户名或密码错误!，请重新输入");
                login.txbName.Text = string.Empty;
                login.txtPassWord.Password = string.Empty;
                return;
            }
        }


        void testObject(object sender, SystemManagerService.ListStudentsCompletedEventArgs e)
        {
            List<Student> stuents = new List<Student>();
            JArray stuArry = JArray.Parse(e.Result);
            foreach (JObject item in stuArry)
            {
                stuents.Add(new Student
                {
                    Name = Convert.ToString(item["Name"]),
                    Age = Convert.ToInt32(item["Age"].ToString())
                });
            }
        }
    }

}
