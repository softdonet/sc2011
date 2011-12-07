using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


using System.Windows.Markup;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace Scada.Client.UC.CommClass
{

    [ContentProperty("Subcomponents")] //声明可在XAML文件中显示的内容属性

    public class Feature : INotifyPropertyChanged //继承接口INotifyPropertyChanged用于双向数据绑定
    {
        //Feature对象的属性

        public string FeatureName { get; set; }

        public string Description { get; set; }


        //声明全局变量

        public Collection<Feature> Subcomponents { get; private set; }

        private bool? _shouldInstall;

        //是否有子组件

        public bool HasSubcomponents
        {
            get
            {
                return Subcomponents.Count > 0;
            }
        }



        //是否允许Feature进行安置

        public bool? ShouldInstall
        {
            get
            {
                return _shouldInstall;
            }

            set
            {
                if (value != _shouldInstall)
                {
                    _shouldInstall = value;
                    OnPropertyChanged("ShouldInstall");
                }
            }
        }



        //构造函数

        public Feature()
        {
            Subcomponents = new Collection<Feature>();
            ShouldInstall = true;
        }

        //事件委托

        public event PropertyChangedEventHandler PropertyChanged;

        //实现接口INotifyPropertyChanged定义函数

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

