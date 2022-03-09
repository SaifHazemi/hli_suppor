using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using Support_Hli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Support_Hli.Controllers
{
    public class ContactController : ApiController
    {
        List<Contact> contacts = new List<Contact>();


        public List<Contact> Retrieve()
        {
            var connectionString = @"AuthType = Office365; 
                            Url =https://xrm-stagiaire.crm-hlitn.com/crmstagier;
                            Username=se.hazemi;
                            Password=Azerty@123+";
            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            IOrganizationService organizationService = (IOrganizationService)crmServiceClient.
                  OrganizationWebProxyClient != null ? (IOrganizationService)crmServiceClient.OrganizationWebProxyClient : (IOrganizationService)crmServiceClient.OrganizationServiceProxy;
            var query = new QueryExpression("contact") { ColumnSet = new ColumnSet("lastname", "new_email", "telephone1", "contactid", "new_password") };

            EntityCollection eccontact = organizationService.RetrieveMultiple(query);
            for (int i = 0; i < eccontact.Entities.Count; i++)
            {
                Contact contact = new Contact();
                if (eccontact.Entities[i].Contains("lastname"))
                {
                    contact.full_name = (string)eccontact.Entities[i].Attributes["lastname"];
                }
                if (eccontact.Entities[i].Contains("new_email"))
                {
                    contact.Email = (string)eccontact.Entities[i].Attributes["new_email"];
                }
                if (eccontact.Entities[i].Contains("telephone1"))
                {
                    contact.Business_phone = (string)eccontact.Entities[i].Attributes["telephone1"];
                }
                //contact.password = (string)eccontact.Entities[i].Attributes["new_password"];
                if (eccontact.Entities[i].Contains("contactid"))
                {
                    contact.ContactID = (Guid)eccontact.Entities[i].Attributes["contactid"];
                }
                if (eccontact.Entities[i].Contains("new_password"))
                {
                    contact.password = (string)eccontact.Entities[i].Attributes["new_password"];
                }
                contacts.Add(contact);

            }
            return contacts;
        }

        // GET api/<controller>
        public List<Contact> Get()
        {
            contacts = Retrieve();
            return contacts;
        }



        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}