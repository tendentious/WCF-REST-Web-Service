using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Xml;
using System.ServiceModel.Activation;   


namespace UserService
{ 
    //Interface Used to define the contract of the webservice
    [ServiceContract]                                                                  
    
    public interface IUserService
    {
        //Registered WebService Methods callable by the client
        [OperationContract]
        [WebGet(UriTemplate = "users", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]                                           
        User[] GetAllUsers();

        [OperationContract]
        [WebInvoke(UriTemplate = "users", Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string UpdateUser(User user);

        [OperationContract]
        [WebInvoke(UriTemplate = "users/{id}", Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string DeleteUser(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "users", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string AddUser(User user);
       

        
    }
    
    //Data to be exchanged between the Web Service and the client
    [DataContract]
    public class User
    {    
        [DataMember]
        public string id { get; set; }     


        [DataMember]
        public string fullname { get; set; }     


        [DataMember]
        public string status { get; set; }   
      
    }  

}
