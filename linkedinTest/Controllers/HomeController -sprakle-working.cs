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
            var scope = (AuthorizationScope)"r_fullprofile%20r_emailaddress%20w_share";// AuthorizationScope.ReadBasicProfile | AuthorizationScope.ReadEmailAddress;
            var state = Guid.NewGuid().ToString();
            var redirectUrl = "http://localhost:55840/Home/OAuth2";
            var url = api.OAuth2.GetAuthorizationUrl(scope, state, redirectUrl);
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
            else
            {
                try
                {
                    api = new LinkedInApi(config);
                    var redirectUrl = "http://localhost:55840/Home/OAuth2"; // Url.Action("OAuth2");
                    var userToken = await api.OAuth2.GetAccessTokenAsync(code, redirectUrl);
                    var user = new UserAuthorization(userToken.AccessToken);
                    string[] acceptlangs = { "en-US" };
                    var profile = api.Profiles.GetMyProfile(user, acceptlangs);//, acceptlangs, FieldSelector.For<Person>().WithAllFields());
                    code = string.Empty;
                    userToken.AccessToken = string.Empty;
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    // throw new HttpRequestException("Request to linkedin Service failed.");
                }
            }

            return View();
        }

    }
}


