using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using Sparkle.LinkedInNET.OAuth2;
using Sparkle.LinkedInNET;
using System.Threading.Tasks;
using Sparkle.LinkedInNET.Profiles;

namespace linkedinTest.Controllers
{
    public class HomeController : Controller
    {
        // create a configuration object
        LinkedInApiConfiguration config = new LinkedInApiConfiguration("81u03um2b7wur4", "oS1GvGtBruhC4s75");
        // get the APIs client
        LinkedInApi api;
        public ActionResult Index()
        {

            api = new LinkedInApi(config);
            var scope = AuthorizationScope.ReadBasicProfile | AuthorizationScope.ReadEmailAddress;
            var state = Guid.NewGuid().ToString();
            var redirectUrl = "http://localhost:55840/Home/OAuth2";
            var url = api.OAuth2.GetAuthorizationUrl(scope, state, redirectUrl);
            // https://www.linkedin.com/uas/oauth2/authorization?response_type=code&client_id=...
            // now redirect your user there
            return Redirect(url.ToString());

        }
        public async Task<ActionResult> OAuth2(string code, string state, string error, string error_description)
        {
            if (!string.IsNullOrEmpty(error))
            {
                this.ViewBag.Error = error;
                this.ViewBag.ErrorDescription = error_description;
                return View();
            }
            api = new LinkedInApi(config);
            var redirectUrl = "http://localhost:55840/Home/OAuth2"; // Url.Action("OAuth2");
            ////replacing below code to get accesstoken instead of **B
            //var client = new RestClient("https://www.linkedin.com/oauth/v2/accessToken") { };
            //var authRequest = new RestRequest("", Method.GET) { };
            //authRequest.AddHeader("Content-Type", "x-www-form-urlencoded");
            //authRequest.AddParameter("grant_type", "authorization_code");
            //authRequest.AddParameter("code", code);
            //authRequest.AddParameter("redirect_uri", redirectUrl);
            //authRequest.AddParameter("client_id", "81u03um2b7wur4");
            //authRequest.AddParameter("client_secret", "oS1GvGtBruhC4s75");
            //authRequest.AddParameter("scope", "r_liteprofile%20r_emailaddress%20w_member_social");
            //authRequest.AddParameter("state", Guid.NewGuid().ToString());

            var userToken = await api.OAuth2.GetAccessTokenAsync(code, redirectUrl);

            try
            {
                var response = userToken;// client.Execute(authRequest);

                //if call failed ErrorResponse created...simple class with response properties
                //if (!response.IsSuccessful)
                //{
                //    var errorData =  response.Content.ToString();
                //    //    ErrorResponse errResp = JsonConvert.DeserializeObject<ErrorResponse>(error);
                //    //    throw new PayPalException { error_name = errResp.name, details = errResp.details, message = errResp.message };
                //}

                //var success =  response.Content.ToString();
              //  var result = JsonConvert.DeserializeObject<Models.LinkedInTokenResponseModel>(success);

               //**B// var user = new UserAuthorization(response.ToString());
                var user = new UserAuthorization(userToken.AccessToken);
              string[] acceptlangs = {"en-US"};
                var profile =  api.Profiles.GetMyProfile(user,acceptlangs);//, acceptlangs, FieldSelector.For<Person>().WithAllFields());

                //for our own desired fieldsets
                try
                {
                // var profile = this.api.Profiles.GetMyProfile(user , acceptlangs, FieldSelector.For<Person>().WithAllFields());
                    var Profileclient = new RestClient("https://api.linkedin.com/v2/me") { };
                    var ProfileAuthRequest = new RestRequest("", Method.GET) { };                   
                    ProfileAuthRequest.AddHeader("Authorization", "Bearer "+ userToken.AccessToken);
                    ProfileAuthRequest.AddParameter("scope", "r_liteprofile%20r_emailaddress%20w_member_social");
                    var Profileresponse = Profileclient.Execute(ProfileAuthRequest);

                }
                catch (LinkedInApiException ex) // one exception type to handle
                {
                    Response.Write(ex.Message);
                    // ex.InnerException // WebException
                    // however it is not recommended to specify all fields
                }


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                throw new HttpRequestException("Request to linkedin Service failed.");
            }

            //**B   //var result = await this.api.OAuth2.GetAccessTokenAsync(code, redirectUrl); Commenting because giving me error
            //this.ViewBag.Code = code;
            //this.ViewBag.Token = authResponse;  //**B   //result.AccessToken;
            //this.data.SaveAccessToken(result.AccessToken);
            //**B   var user = new UserAuthorization(result.AccessToken);
           


            ////var profile = this.api.Profiles.GetMyProfile(user);
            ////this.data.SaveAccessToken();
            return View();
        }


        

    }
}


