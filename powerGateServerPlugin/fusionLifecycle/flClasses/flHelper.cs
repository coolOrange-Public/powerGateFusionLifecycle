using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using System.Net;

namespace fusionLifecycle
{
    class FlHelper
    {
        //string tenant = "coolorange";
        //string user = "marco.mirandola@coolorange.com";
        //string password = "i40Ladroni";
        FlLogin login = null;
        CookieContainer _cookieJar = new CookieContainer();
        RestClient client = null;
        private static FlHelper _instance = null;
        public static FlHelper instance
        {
            get
            {
                if (_instance == null) _instance = new FlHelper();
                return _instance;
            }
        }


        public void Login(string tenant, string user, string password)
        {
            client = new RestClient(String.Format("https://{0}.autodeskplm360.net",tenant));
            //client.Authenticator = new HttpBasicAuthenticator("marco.mirandola@coolorange.com", "i40Ladroni");
            var request = new RestRequest("/rest/auth/1/login", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            FlCredentials credentials = new FlCredentials { UserId=user, Password=password };
            var credJson = JsonConvert.SerializeObject(credentials);
            request.AddParameter("application/json", credJson, ParameterType.RequestBody);


            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            //request.AddFile(path);

            // execute the request
            var response = client.Execute(request);
            login = JsonConvert.DeserializeObject<FlLogin>(response.Content);
            var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            //RestResponse<Person> response2 = client.Execute<Person>(request);
            //var name = response2.Data.Name;
            string baseUri = String.Format("https://{0}.autodeskplm360.net", tenant);
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(new Uri(baseUri), new Cookie() { Name = "customer", Path = "/", Value = login.CustomerToken, Expires = DateTime.Now.AddDays(1d) });
            cookieContainer.Add(new Uri(baseUri), new Cookie() { Name = "JSESSIONID", Path = "/", Value = login.Sessionid, Expires = DateTime.Now.AddDays(1d) });
            client.CookieContainer = cookieContainer;
        }

        public FlWorkspaces GetWorkspaces()
        {
            var request = new RestRequest("/api/rest/v1/workspaces", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            var response = client.Execute(request);
            var workspaces = JsonConvert.DeserializeObject<FlWorkspaces>(response.Content);
            return workspaces;
        }

        public FlItems GetItems(long workspaceId)
        {
            var request = new RestRequest(String.Format("/api/rest/v1/workspaces/{0}/items", workspaceId), Method.GET);
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            var response = client.Execute(request);
            var items = JsonConvert.DeserializeObject<FlItems>(response.Content);
            return items;
        }

        public FlListItem GetItem(long workspaceId, long Id)
        {
            var request = new RestRequest(String.Format("/api/rest/v1/workspaces/{0}/items/{1}", workspaceId, Id), Method.GET);
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            var response = client.Execute(request);
            var item = JsonConvert.DeserializeObject<FlListItem>(response.Content);
            return item;
        }

        public FlListItem AddItem(long workspaceId, Dictionary<string,string> properties)
        {
            var newFlItem = new NewFlItem();
            if (newFlItem.MetaFields == null) newFlItem.MetaFields = new NewFlItemMetaFields();
            foreach (var prop in properties)
                newFlItem.MetaFields.Entry.Add(new NewFlItemMetaFieldsEntry() { Key = prop.Key, Value = prop.Value });
            var request = new RestRequest(String.Format("/api/rest/v1/workspaces/{0}/items/", workspaceId), Method.POST);
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddJsonBody(newFlItem);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            var item = JsonConvert.DeserializeObject<FlListItem>(response.Content);
            return item;
        }
    }


}
