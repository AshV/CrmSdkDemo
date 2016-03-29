using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace CrmSdkDemo
{
    class QueryExpressionDemo
    {
        static void Main(string[] args)
        {
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = ConfigurationManager.AppSettings["UID"];
            credentials.UserName.Password = ConfigurationManager.AppSettings["PWD"];
            using (var _serviceProxy = new OrganizationServiceProxy(new Uri(ConfigurationManager.AppSettings["URI"]), null, credentials, null))
            {
                QueryExpression qe = new QueryExpression();
                qe.EntityName = "account";
                qe.ColumnSet = new ColumnSet();
                qe.ColumnSet.Columns.Add("name");

                EntityCollection ec = _serviceProxy.RetrieveMultiple(qe);

                Console.WriteLine("Retrieved {0} entities", ec.Entities.Count);

                foreach (Entity act in ec.Entities)
                {
                    Console.WriteLine(act["name"]);
                }
            }
        }
    }
}