using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace CrmSdkDemo
{
    class EarlyBoundDemo
    {
        static void Main(string[] args)
        {
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = ConfigurationManager.AppSettings["UID"];
            credentials.UserName.Password = ConfigurationManager.AppSettings["PWD"];
            using (var _serviceProxy = new OrganizationServiceProxy(new Uri(ConfigurationManager.AppSettings["URI"]), null, credentials, null))
            {
                // This statement is required to enable early-bound type support.
                _serviceProxy.EnableProxyTypes();

                // Creation
                Account paccount = new Account { Name = "Sample Parent Account" };
                Guid _parentAccountId = _serviceProxy.Create(paccount);
                Account account = new Account
                {
                    Name = "RBT Early Bound",
                    EMailAddress1 = "ashish6318@gmail.com"
                };
                Guid _accountId = _serviceProxy.Create(account);
                Console.WriteLine("{0} {1} created, ", account.LogicalName, account.Name);

                // Retrieve the account containing several of its attributes.
                ColumnSet cols = new ColumnSet(new String[] { "name", "address1_postalcode", "lastusedincampaign", "versionnumber" });
                Account retrievedAccount = (Account)_serviceProxy.Retrieve("account", _accountId, cols);
                Console.Write("retrieved ");
                string eMailAddress1 = retrievedAccount.EMailAddress1;
                Console.WriteLine("EMail Address : {0}, ", eMailAddress1);

                //Updation
                retrievedAccount.Address1_PostalCode = "98052";
                retrievedAccount.Address2_PostalCode = null;
                retrievedAccount.Address1_AddressTypeCode = new OptionSetValue((int)3);
                retrievedAccount.Address1_ShippingMethodCode = new OptionSetValue((int)AccountAddress1_ShippingMethodCode.DHL);
                retrievedAccount.IndustryCode = new OptionSetValue((int)AccountIndustryCode.AgricultureandNonpetrolNaturalResourceExtraction);

                // Shows use of a Money value.
                retrievedAccount.Revenue = new Money(5000000);

                // Shows use of a Boolean value.
                retrievedAccount.CreditOnHold = false;

                // Shows use of EntityReference.
                retrievedAccount.ParentAccountId = new EntityReference(Account.EntityLogicalName, _parentAccountId);

                // Shows use of Memo attribute.
                retrievedAccount.Description = "Account for RBT";

                // Update the account record.
                _serviceProxy.Update(retrievedAccount);
                Console.WriteLine("Record Updated.");

                //Deletion
                _serviceProxy.Delete("account", _parentAccountId);
                _serviceProxy.Delete("account", _accountId);
                Console.WriteLine("Entity record(s) have been deleted.");
            }
        }
    }
}
