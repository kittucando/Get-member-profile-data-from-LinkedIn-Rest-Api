using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sparkle.LinkedInNET;
using System.Threading.Tasks;
using Sparkle.LinkedInNET.Profiles;
using linkedinTest.Models;

namespace linkedinTest.Controllers
{
    public class HomeController : Controller
    {
        // create a configuration object
        LinkedInApiConfiguration config = new LinkedInApiConfiguration("81u03um2b7wur8", "oS1GvGtBruhC4s78");

        // use base url and mention the page name where it has to redirect back after authorization. DONT USE RELATIVE PATH
        public string redirect_uri = "http://localhost:55840/Home/OAuth2";

        public ActionResult Index()
        {
            //1st Step: Build Linkedin login authorize url and redirect
            string encodedurl = GetLinkedAuthorizeUrl();
            // now redirect your user there(linkedin)
            return Redirect(encodedurl);

        }       

        public async Task<ActionResult> OAuth2(string code, string state, string error, string error_description)
        {
            //2 Step:After logging in, linkedin will redirect back to our site, to the url that whe mentioned in reurn_uri, with Authorize Code or error
            Person profile = null;
            LinkedInProfile linkedInProfile = new LinkedInProfile();

            if (!string.IsNullOrEmpty(error) | !string.IsNullOrEmpty(error_description))
            {
                this.ViewBag.Error = error;
                this.ViewBag.ErrorDescription = error_description;
                return View();
            }
            else
            {
                //3rd step: If no errors, Get the AccessToken from the AuthorizeCode that linkedin sent back to us       
                try
                {
                    // get the APIs client to get the accesstoken
                    LinkedInApi api = new LinkedInApi(config);
                    var userToken = await api.OAuth2.GetAccessTokenAsync(code, redirect_uri);

                    //4th step: Use this access token to get the loggedin Member details
                    if (userToken != null && !string.IsNullOrEmpty(userToken.AccessToken))
                    {

                 //1-way to get member profile details: Conventional way  // Not giving all the values for the specified scopes
                        //var Profileclient = new RestClient("https://api.linkedin.com/v2/me?projection=(id,firstName,lastName,title,position,profilePicture,displayImage,profilePicture(displayImage~:playableStreams))") { };
                        //var ProfileAuthRequest = new RestRequest("", Method.GET) { };
                        //ProfileAuthRequest.AddHeader("Authorization", "Bearer " + userToken.AccessToken);
                        //var Profileresponse = Profileclient.Execute(ProfileAuthRequest);

                 //2-way to get member profile details: Through  Sparkle.LinkedInNET plugin
                        var user = new UserAuthorization(userToken.AccessToken);
                        string[] acceptlangs = { "en-US" };// need to pass the accepting languages
                        profile = api.Profiles.GetMyProfile(user, acceptlangs, FieldSelector.For<Person>().WithEmailAddress().WithId().WithPictureUrl().WithPositionsTitle().WithSummary().WithFirstName().WithLastName().WithMaidenName().WithPhoneNumbers().WithPublicProfileUrl());

                
                        //5th step: After getting the profile details, map to our own model
                        if (profile != null)
                        {
                            //Map return values to our own model
                            linkedInProfile.Firstname = profile.Firstname;
                            linkedInProfile.Lastname = profile.Lastname;
                            linkedInProfile.MaidenName = profile.MaidenName;
                            linkedInProfile.EmailAddress = profile.EmailAddress;
                            linkedInProfile.PictureUrl = profile.PictureUrl;
                            linkedInProfile.PublicProfileUrl = profile.PublicProfileUrl;
                            linkedInProfile.Summary = profile.Summary;
                            if (profile.Positions != null)
                            {
                                PersonPosition personpos = profile.Positions.Position.FirstOrDefault() != null ? profile.Positions.Position.SingleOrDefault() : new PersonPosition();
                                linkedInProfile.PositionTitle = personpos.Title ?? string.Empty;
                            }
                            if (profile.PhoneNumbers != null)
                            {
                                PhoneNumber phonenum = profile.PhoneNumbers.PhoneNumber.Count > 0 ? profile.PhoneNumbers.PhoneNumber.SingleOrDefault() : new PhoneNumber();
                                linkedInProfile.PhoneNumber = phonenum.Number ?? string.Empty;
                            }

                        }



                    }


                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    // throw new HttpRequestException("Request to linkedin Service failed.");
                }
            }

            return View(linkedInProfile);
        }

        private string GetLinkedAuthorizeUrl()
        {
            string scope = "r_liteprofile r_basicprofile r_emailaddress";
            //var scope = AuthorizationScope.ReadBasicProfile | AuthorizationScope.ReadEmailAddress  ;uncomment when sparkle is used
            string state = Guid.NewGuid().ToString();           
            string lnAuthorizeApi = "https://www.linkedin.com/oauth/v2/authorization";           

            string CompleteAuthorizeUrl = lnAuthorizeApi + "?response_type=code&client_id=" + config.ApiKey + "&redirect_uri=" + redirect_uri + "&state=" + state + "&scope=" + scope;
            string encodedurl = HttpUtility.UrlPathEncode(CompleteAuthorizeUrl);
            //string encodedurl="https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id=81u03um2b7wur4&redirect_uri=http%3A%2F%2Flocalhost%3A55840%2FHome%2FOAuth2&state=fooobar&scope=r_liteprofile%20r_basicprofile%20r_emailaddress" ; 
            // api.OAuth2.GetAuthorizationUrl(scope, state, redirectUrl); //When sparkle used
            return encodedurl;
        }
    }
}


