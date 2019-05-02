using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace linkedinTest
{
    public class ImplementingWays
    {


        //[HttpGet]
        //public async Task<ActionResult> OAuth2(string code, string state)
        //{


        //    return View();
        //}


        // GET: LinkedIn
        //// http://mywebsite/LinkedIn/OAuth2?code=...&state=...
        //[HttpGet]
        //public async Task<ActionResult> OAuth2(string code, string state, string error, string error_description)
        //{
        //    if (!string.IsNullOrEmpty(error) || !string.IsNullOrEmpty(error_description))
        //    {
        //        // handle error and error_description
        //    }
        //    else
        //    {
        //        var redirectUrl = this.Request.Compose() + this.Url.Action("OAuth2");
        //        var redirectUrl = "http://localhost:55840/Home/OAuth2";
        //        var userToken = await api.OAuth2.GetAccessTokenAsync(code, redirectUrl);
        //        var user = new UserAuthorization(userToken.AccessToken);
        //        // var profile = api.Profiles.GetMyProfile(user);
        //        // keep this token for your API requests

        //        // 1.way
        //        //        var profile = api.Profiles.GetMyProfile(
        //        //user,
        //        //FieldSelector.For<Person>().WithFirstname().WithLastname().WithLocationName());
        //        //        // https://api.linkedin.com/v1/people/~:(first-name,last-name,location:(name))
        //        //    };

        //        //2.way
        //        //for our own desired fieldsets
        //        //        var profile = api.Profiles.GetMyProfile(
        //        //user,
        //        //FieldSelector.For<Person>().WithAllFields());
        //        //        // https://api.linkedin.com/v1/people/~:(all available fields here)
        //        //        // however it is not recommended to specify all fields
        //        try
        //        {
        //            var profile = this.api.Profiles.GetMyProfile(user, null, FieldSelector.For<Person>().WithAllFields());
        //        }
        //        catch (LinkedInApiException ex) // one exception type to handle
        //        {
        //            Response.Write(ex.Message);
        //            // ex.InnerException // WebException
        //            // ex.Data["ResponseStream"]
        //            // ex.Data["HttpStatusCode"]
        //            // ex.Data["Method"]
        //            // ex.Data["UrlPath"]
        //            // ex.Data["ResponseText"]
        //        }
        //    }
        //    return View();
        //}

        //public ActionResult Method2()//no use
        //{
        //    if (ServicePointManager.SecurityProtocol != SecurityProtocolType.Tls12) ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // forced to modern day SSL protocols
        //    var client = new RestClient("https://www.linkedin.com/oauth/v2/accessToken") { };
        //    var authRequest = new RestRequest("", Method.GET) { };
        //    // client.Authenticator = new HttpBasicAuthenticator(username, password);
        //    authRequest.AddParameter("grant_type", "client_credentials");
        //    authRequest.AddParameter("code", "oS1GvGtBruhC4s75");
        //    authRequest.AddParameter("redirect_uri", "oS1GvGtBruhC4s75");
        //    authRequest.AddParameter("client_id", "81u03um2b7wur4");
        //    authRequest.AddParameter("client_secret", "oS1GvGtBruhC4s75");
        //    var authResponse = client.Execute(authRequest);

        //    return View();
        //}
        //// GET: Default
        //public ActionResult Index()
        //{
        //    //  var returnresp= RequestPayPalToken(username, password, PostUrl);
        //    if (ServicePointManager.SecurityProtocol != SecurityProtocolType.Tls12) ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // forced to modern day SSL protocols
        //    var client = new RestClient("https://www.linkedin.com/oauth/v2/authorization") {  };
        //    var authRequest = new RestRequest("", Method.GET) {  };
        //   // client.Authenticator = new HttpBasicAuthenticator(username, password);
        //    authRequest.AddParameter("response_type", "code");
        //    authRequest.AddParameter("client_id", "81u03um2b7wur4");
        //    authRequest.AddParameter("redirect_uri", "http://localhost:55840/Home/Index");
        //    authRequest.AddParameter("state", "fooobar");
        //    authRequest.AddParameter("scope", "r_liteprofile%20r_emailaddress%20w_member_social"); 
        //    var authResponse = client.Execute(authRequest);  
        //    return View();
        //}
        //{ string URL = "https://www.linkedin.com/oauth/v2/authorization";
        //string DATA = @"{""response_type"":""code"",""client_id"":""81u03um2b7wur4"",""redirect_uri"":""http://localhost:55840/Home/Index"",""state"":""fooobar"",""scope"":""r_liteprofile%20r_emailaddress%20w_member_social""}";
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
        //request.Method = "POST";
        //           // request.ContentType = "application/json";
        //           request.ContentLength = DATA.Length;
        //            using (Stream webStream = request.GetRequestStream())
        //            using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
        //            {
        //                requestWriter.Write(DATA);
        //            }
        //            try
        //            {
        //                WebResponse webResponse = request.GetResponse();
        //                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
        //                using (StreamReader responseReader = new StreamReader(webStream))
        //                {
        //                    string response = responseReader.ReadToEnd();
        //                    //Console.Out.WriteLine(response);
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //              Response.Write(e.Message);
        //                //Console.Out.WriteLine("-----------------");
        //                //Console.Out.WriteLine(e.Message);
        //            }
        //}
        //    {
        //   //string URL = "https://sub.domain.com/objects.json";
        //            //string urlParameters = "?api_key=123";
        //            //HttpClient client = new HttpClient();
        //            //client.BaseAddress = new Uri(URL);
        //            //// Add an Accept header for JSON format.
        //            //client.DefaultRequestHeaders.Accept.Add(
        //            //    new MediaTypeWithQualityHeaderValue("application/json"));
        //            //// List data response.
        //            //HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
        //            //if (response.IsSuccessStatusCode)
        //            //{
        //            //    // Parse the response body.
        //            //    var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
        //            //    foreach (var d in dataObjects)
        //            //    {
        //            //        Console.WriteLine("{0}", d.Name);
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        //            //}
        //}

    }
}