﻿using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;



namespace Scada.Server.Web
{
    [ServiceContract(Namespace = "")]
    [SilverlightFaultBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RIAService
    {
        [OperationContract]
        public void DoWork()
        {
            // 在此处添加操作实现
            return;
        }

        // 在此处添加更多操作并使用 [OperationContract] 标记它们
    }
}
