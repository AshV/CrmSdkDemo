using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Configuration;
using System.ServiceModel.Description;

namespace CrmSdkDemo
{
    class CreateEntityDemo
    {
        static void Main(string[] args)
        {
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = ConfigurationManager.AppSettings["UID"];
            credentials.UserName.Password = ConfigurationManager.AppSettings["PWD"];

            String _customEntityName = "new_rbt";

            using (var _serviceProxy = new OrganizationServiceProxy(new Uri(ConfigurationManager.AppSettings["URI"]), null, credentials, null))
            {
                CreateEntityRequest createrequest = new CreateEntityRequest
                {

                    //Define the entity
                    Entity = new EntityMetadata
                    {
                        SchemaName = "new_Rbt",
                        DisplayName = new Label("RBT Entity", 1033),
                        DisplayCollectionName = new Label("RBT Entities", 1033),
                        Description = new Label("Demo Entity", 1033),
                        OwnershipType = OwnershipTypes.UserOwned,
                        IsActivity = false,

                    },

                    // Define the primary attribute for the entity
                    PrimaryAttribute = new StringAttributeMetadata
                    {
                        SchemaName = "new_rbtname",
                        RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                        MaxLength = 100,
                        FormatName = StringFormatName.Text,
                        DisplayName = new Label("RBT Name", 1033),
                        Description = new Label("The primary attribute for the RBT entity.", 1033)
                    }



                };
                _serviceProxy.Execute(createrequest);
                Console.WriteLine("The RBT entity has been created.");


                // Add some attributes to the New entity
                CreateAttributeRequest createBankNameAttributeRequest = new CreateAttributeRequest
                {
                    EntityName = _customEntityName,
                    Attribute = new StringAttributeMetadata
                    {
                        SchemaName = "new_branchname",
                        RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                        MaxLength = 100,
                        FormatName = StringFormatName.Text,
                        DisplayName = new Label("Branch Name", 1033),
                        Description = new Label("The name of the Branch.", 1033)
                    }
                };

                _serviceProxy.Execute(createBankNameAttributeRequest);
                Console.WriteLine("An branch name attribute has been added to the RBT entity.");
            }
        }
    }
}