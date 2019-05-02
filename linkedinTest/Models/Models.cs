using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace linkedinTest.Models
{
    public class LinkedInTokenResponseModel
    {
        public string scope { get; set; }
        public string nonce { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string app_id { get; set; }
        public int expires_in { get; set; }
    }

    public class LinkedInProfile
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MaidenName { get; set; }
        public string EmailAddress { get; set; }
        public string PositionTitle { get; set; }       
        public string PhoneNumber { get; set; }
        public string PictureUrl { get; set; }
        public string PublicProfileUrl { get; set; }
        public string Summary { get;  set; }
    }
}