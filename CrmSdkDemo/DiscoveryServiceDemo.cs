﻿using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel.Description;

// Demo for RetrieveOrganizationRequest & RetrieveOrganizationsRequest

namespace CrmSdkDemo
{
    class DiscoveryServiceDemo
    {
        static void Main(string[] args)
        {
            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = ConfigurationManager.AppSettings["UID"];
            credentials.UserName.Password = ConfigurationManager.AppSettings["PWD"];
            using (var service = new DiscoveryServiceProxy(new Uri(ConfigurationManager.AppSettings["DURI"]), null, credentials, null))
            {
                {
                    #region RetrieveOrganizations Message

                    //<snippetDiscoveryService2>
                    // Retrieve details about all organizations discoverable via the
                    // Discovery service.
                    RetrieveOrganizationsRequest orgsRequest =
                        new RetrieveOrganizationsRequest()
                        {
                            AccessType = EndpointAccessType.Default,
                            Release = OrganizationRelease.Current
                        };
                    RetrieveOrganizationsResponse organizations =
                        (RetrieveOrganizationsResponse)service.Execute(orgsRequest);
                    //</snippetDiscoveryService2>

                    // Print each organization's friendly name, unique name and URLs
                    // for each of its endpoints.
                    Console.WriteLine();
                    Console.WriteLine("Retrieving details of each organization:");
                    foreach (OrganizationDetail organization in organizations.Details)
                    {
                        Console.WriteLine("Organization Name: {0}", organization.FriendlyName);
                        Console.WriteLine("Unique Name: {0}", organization.UniqueName);
                        Console.WriteLine("Endpoints:");
                        foreach (var endpoint in organization.Endpoints)
                        {
                            Console.WriteLine("  Name: {0}", endpoint.Key);
                            Console.WriteLine("  URL: {0}", endpoint.Value);
                        }
                    }
                    Console.WriteLine("End of listing");
                    Console.WriteLine();

                    #endregion RetrieveOrganizations Message

                    #region RetrieveOrganization Message

                    //<snippetDiscoveryService3>
                    // Retrieve details about a single organization discoverable via the Discovery service.
                    //
                    RetrieveOrganizationRequest orgRequest =new RetrieveOrganizationRequest()
                        {
                            UniqueName = organizations.Details[organizations.Details.Count - 1].UniqueName,
                            AccessType = EndpointAccessType.Default,
                            Release = OrganizationRelease.Current
                        };
                    RetrieveOrganizationResponse org =
                        (RetrieveOrganizationResponse)service.Execute(orgRequest);
                    //</snippetDiscoveryService3>

                    // Print the organization's friendly name, unique name and URLs
                    // for each of its endpoints.
                    Console.WriteLine();
                    Console.WriteLine("Retrieving details of specific organization:");
                    Console.WriteLine("Organization Name: {0}", org.Detail.FriendlyName);
                    Console.WriteLine("Unique Name: {0}", org.Detail.UniqueName);
                    Console.WriteLine("Endpoints:");
                    foreach (KeyValuePair<EndpointType, string> endpoint in org.Detail.Endpoints)
                    {
                        Console.WriteLine("  Name: {0}", endpoint.Key);
                        Console.WriteLine("  URL: {0}", endpoint.Value);
                    }
                    Console.WriteLine("End of listing");
                    Console.WriteLine();

                    #endregion RetrieveOrganization Message

                }
            }
        }
    }
}
