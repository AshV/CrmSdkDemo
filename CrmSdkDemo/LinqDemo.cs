using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Description;

namespace CrmSdkDemo
{
    class LinqDemo
    {
        static void Main(string[] args)
        {
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = ConfigurationManager.AppSettings["UID"];
            credentials.UserName.Password = ConfigurationManager.AppSettings["PWD"];
            using (var _serviceProxy = new OrganizationServiceProxy(new Uri(ConfigurationManager.AppSettings["URI"]), null, credentials, null))
            {
                {
                    // This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes();
                    var _service = (IOrganizationService)_serviceProxy;


                    // Create the ServiceContext object that will generate
                    // the IQueryable collections for LINQ calls.
                    ServiceContext svcContext = new ServiceContext(_service);
                    // Loop through all CRM account using the IQueryable interface
                    // on the ServiceContext object
                    //<snippetCreateALinqQuery1>
                    var accounts = from a in svcContext.AccountSet
                                   select new Account
                                   {
                                       Name = a.Name,
                                       Address1_County = a.Address1_County
                                   };
                    System.Console.WriteLine("List all accounts in CRM\n");
                    foreach (var a in accounts)
                    {
                        System.Console.WriteLine(a.Name + " " + a.Address1_County);
                    }

                    accounts = from a in svcContext.AccountSet
                               where a.Name.Contains("Fourth")
                               select new Account
                               {
                                   Name = a.Name,
                                   Address1_County = a.Address1_County
                               };
                    System.Console.WriteLine("\nList accounts with Specific CRM\n");
                    foreach (var a in accounts)
                    {
                        System.Console.WriteLine(a.Name + " " + a.Address1_County);
                    }
                }
            }
        }
    }
}