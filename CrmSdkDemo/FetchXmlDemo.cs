using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace CrmSdkDemo
{
    class FetchXmlDemo
    {
        static void Main(string[] args)
        {
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = ConfigurationManager.AppSettings["UID"];
            credentials.UserName.Password = ConfigurationManager.AppSettings["PWD"];
            using (var _serviceProxy = new OrganizationServiceProxy(new Uri(ConfigurationManager.AppSettings["URI"]), null, credentials, null))
            {
                var fetchXml =
                    @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
  <entity name='account'>
    <attribute name='name' />
    <attribute name='primarycontactid' />
    <attribute name='telephone1' />
    <attribute name='accountid' />
    <order attribute='name' descending='false' />
  </entity>
</fetch>";

                // Run the query with the FetchXML.
                var fetchExpression = new FetchExpression(fetchXml);
                EntityCollection fetchResult = _serviceProxy.RetrieveMultiple(fetchExpression);

                // Display the results.
                Console.WriteLine("Retrieved Data");
                foreach (Entity entity in fetchResult.Entities)
                {
                    var account = entity.ToEntity<Account>();
                    Console.WriteLine("Account ID: {0}", account.Id);
                    Console.WriteLine("Account: {0}", account.Name);
                }
            }
        }
    }
}