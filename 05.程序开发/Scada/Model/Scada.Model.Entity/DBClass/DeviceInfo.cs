using System;
using System.Collections.Generic;

namespace Scada.Model.Entity
{
    /// <summary>
    /// �豸��Ϣ
    /// </summary>
    public class DeviceInfo
    {
        public Guid ID { get; set; }
        //�豸���
        public string DeviceNo { get; set; }
        //�豸SN��
        public string DeviceSN { get; set; }
        //Ӳ������
        public string HardType { get; set; }
        //��������
        public DateTime? ProductDate { get; set; }
        public string DeviceMAC { get; set; }
        public string SIMNo { get; set; }
        //�������
        public Guid ManageAreaID { get; set; }
        //ά����ԱID
        public Guid MaintenancePeopleID { get; set; }
        //��װ�ص�
        public string InstallPlace { get; set; }
        //����
        public decimal? Longitude { get; set; }
        //ά��
        public decimal? Latitude { get; set; }
        public decimal? High { get; set; }
        public string Comment { get; set; }
        //ƫ��
        public int? Windage { get; set; }
        //Ӳ���汾
        public string HardwareVersion { get; set; }
        //����汾
        public string SoftWareVersion { get; set; }
        //LCD��ʾ����
        public int? LCDScreenDisplayType { get; set; }
        public bool? UrgencyBtnEnable { get; set; }
        public bool? InforBtnEnable { get; set; }
        //���¶�������Ч
        public bool? Temperature1AlarmValid { get; set; }
        //���¶ȸ߱���
        public decimal? Temperature1HighAlarm { get; set; }
        //���¶ȵͱ���
        public decimal? Temperature1LowAlarm { get; set; }
        //���¶�������Ч
        public bool? Temperature2AlarmValid { get; set; }
        //���¶ȸ߱���
        public decimal? Temperature2HighAlarm { get; set; }
        //���¶ȵͱ���
        public decimal? Temperature2LowAlarm { get; set; }
        //ʪ��������Ч
        public bool? HumidityAlarmValid { get; set; }
        //ʪ�ȸ߱���
        public decimal? HumidityHighAlarm { get; set; }
        //ʪ�ȵͱ���
        public decimal? HumidityLowAlarm { get; set; }
        //�ź�������Ч
        public bool? SignalAlarmValid { get; set; }
        public int? SignalHighAlarm { get; set; }
        public int? SignalLowAlarm { get; set; }
        //����������Ч
        public bool? ElectricityAlarmValid { get; set; }
        public int? ElectricityHighAlarm { get; set; }
        public int? ElectricityLowAlarm { get; set; }
        //��ǰģʽ
        public int? CurrentModel { get; set; }
        //ʵʱģʽ����
        public int? RealTimeParam { get; set; }
        //����ģʽ����1
        public int? FullTimeParam1 { get; set; }
        //����ģʽ����2
        public int? FullTimeParam2 { get; set; }
        //����򱨲���1
        public int? OptimizeParam1 { get; set; }
        //����򱨲���1
        public int? OptimizeParam2 { get; set; }
        //����򱨲���1
        public int? OptimizeParam3 { get; set; }
    }
}

