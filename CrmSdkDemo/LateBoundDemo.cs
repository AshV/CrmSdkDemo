using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace CrmSdkDemo
{
    class LateBoundDemo
    {
        static void Main(string[] args)
        {
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = ConfigurationManager.AppSettings["UID"];
            credentials.UserName.Password = ConfigurationManager.AppSettings["PWD"];
            using (var _serviceProxy = new OrganizationServiceProxy(new Uri(ConfigurationManager.AppSettings["URI"]), null, credentials, null))
            {
                var _service = (IOrganizationService)_serviceProxy;
                Entity account = new Entity("account");

                // Creation
                account["name"] = "RBT Record";
                account["address1_postalcode"] = "500081";
                Guid _accountId = _service.Create(account);
                Console.WriteLine("{0} {1} created, ", account.LogicalName, account.Attributes["name"]);

                // Retrieving
                ColumnSet attributes = new ColumnSet(new string[] { "name", "ownerid", "address1_postalcode" });
                account = _service.Retrieve(account.LogicalName, _accountId, attributes);
                Console.WriteLine("Retrieved Data");
                Console.WriteLine(account["address1_postalcode"]);

                // Updation
                account["address1_postalcode"] = "500082";
                account["address2_postalcode"] = null;
                account["revenue"] = new Money(5000000);
                account["creditonhold"] = false;
                _service.Update(account);
                Console.WriteLine("Entity Updated.");

                //Deleting 
                _service.Delete("account", _accountId);
                Console.WriteLine("Entity record(s) have been deleted.");
            }
        }
    }
}