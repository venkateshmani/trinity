using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AssetManagerWebService
{  
    [ServiceContract]
    public interface IAssetManagerService
    {       

        [OperationContract]
        LoginResult Authenticate(string userName, string password);


    }

    [DataContract]
    public class LoginResult
    {
        [DataMember]
        public bool Authenticated
        {
            get;
            set;
        }
        [DataMember]
        public string Message
        {
            get;
            set;
        }
        [DataMember]
        public bool NeedPasswordReset
        {
            get;
            set;
        }

    }
}
