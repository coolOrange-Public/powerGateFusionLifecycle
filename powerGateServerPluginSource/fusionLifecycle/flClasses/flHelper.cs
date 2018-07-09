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
        FlLogin login = null;
        CookieContainer _cookieJar = new CookieContainer();
        RestClient client = null;
        string baseUrl = "";
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
            baseUrl = String.Format("https://{0}.autodeskplm360.net", tenant);
            client = new RestClient(baseUrl);
            var request = new RestRequest("/rest/auth/1/login", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            FlCredentials credentials = new FlCredentials { UserId=user, Password=password };
            var credJson = JsonConvert.SerializeObject(credentials);
            request.AddParameter("application/json", credJson, ParameterType.RequestBody);
            // execute the request
            var response = client.Execute(request);
            login = JsonConvert.DeserializeObject<FlLogin>(response.Content);
            var content = response.Content; // raw content as string

            string baseUri = String.Format("https://{0}.autodeskplm360.net", tenant);
            _cookieJar = new CookieContainer();
            _cookieJar.Add(new Uri(baseUri), new Cookie() { Name = "customer", Path = "/", Value = login.CustomerToken, Expires = DateTime.Now.AddDays(1d) });
            _cookieJar.Add(new Uri(baseUri), new Cookie() { Name = "JSESSIONID", Path = "/", Value = login.Sessionid, Expires = DateTime.Now.AddDays(1d) });
            client.CookieContainer = _cookieJar;
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

        public FlAttachment GetAttachments(long workspaceId, long Id)
        {
            var request = new RestRequest(String.Format("/api/rest/v1/workspaces/{0}/items/{1}/attachments", workspaceId,Id), Method.GET);
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            var response = client.Execute(request);
            var attachements = JsonConvert.DeserializeObject<FlAttachment>(response.Content);
            return attachements;
        }


        private class newAttachment
        {
            public string fileName { get; set; }
            public string description { get; set; }
            public string resourceName { get; set; }
        }
        public void AddAttachment(long workspaceId, long Id, string fileName, string description, System.IO.Stream fileStream)
        {
            string url = String.Format("{0}/api/rest/v1/workspaces/{1}/items/{2}/attachments",baseUrl,workspaceId,Id);
            var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, url);
            var scriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var handler = new System.Net.Http.HttpClientHandler();
            handler.CookieContainer = _cookieJar;
            var ressourceName = fileName + DateTime.Now.ToString();

            var httpContent1 = new System.Net.Http.StringContent(scriptSerializer.Serialize(new newAttachment() { fileName=fileName, description=description, resourceName=fileName }));
            httpContent1.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            var memoryStream = new System.IO.MemoryStream(bytes);

            var httpContent2 = new System.Net.Http.StreamContent(memoryStream);
            httpContent2.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("*");
            httpContent2.Headers.ContentDisposition.FileName = fileName;
            httpContent2.Headers.ContentDisposition.Size = memoryStream.Length;
            var multipart = new System.Net.Http.MultipartContent("mixed");
            multipart.Add(httpContent1);
            multipart.Add(httpContent2);
            request.Content = multipart;
            var httpClient = new System.Net.Http.HttpClient(handler);
            httpClient.BaseAddress = new Uri(url);
            var result1 = httpClient.SendAsync(request).Result;
        }
    }


}
