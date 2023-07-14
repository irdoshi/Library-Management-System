using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace LMS.WebApplication.Controllers
    {
    public class HomeController : Controller
        {

        /* private void UpdateUserInfo(AuthenticationResult authenticationResult)
    {
    this.CurrentUser = new CurrentUser();
    JObject user = this.ParseIdToken(authenticationResult.IdToken);

     this.CurrentUser.AuthToken = authenticationResult.AccessToken;

     this.CurrentUser.FirstName = user["given_name"]?.ToString();
    this.CurrentUser.LastName = user["family_name"]?.ToString();
    this.CurrentUser.UserObjectId = user["oid"]?.ToString();
    var emails = user["emails"] as JArray;
    if (emails != null)
    {
    this.CurrentUser.EmailAddress = emails[0].ToString();
    }
    }

     /// <summary>
    /// Parses the identifier token.
    /// </summary>
    /// <param name="idToken">The identifier token.</param>
    /// <returns>jobject id token.</returns>
    private JObject ParseIdToken(string idToken)
    {
    // Get the piece with actual user info
    idToken = idToken.Split('.')[1];
    idToken = this.Base64UrlDecode(idToken);
    return JObject.Parse(idToken);
    }

     /// <summary>
    /// Base64s the URL decode.
    /// </summary>
    /// <param name="s">The s.</param>
    /// <returns>string base decoded value.</returns>
    private string Base64UrlDecode(string s)
    {
    s = s.Replace('-', '+').Replace('_', '/');
    s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
    var byteArray = Convert.FromBase64String(s);
    var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
    return decoded;
    }*/

        public IActionResult Index()
        {
        var telemetryHelper = new TelemetryClient();
        telemetryHelper.TrackTrace("Tracking Web App");
        telemetryHelper.TrackTrace("Tracking Web App Error", SeverityLevel.Error);
        telemetryHelper.TrackMetric("Page hits", 1);
        return View();
        }
    
            public IActionResult Logout()
            {
            if (AppServicesAuthenticationInformation.IsAppServicesAadAuthenticationEnabled)
                {
                return LocalRedirect(AppServicesAuthenticationInformation.LogoutUrl);
                }
            else
                {
                string scheme = OpenIdConnectDefaults.AuthenticationScheme;
                return SignOut(
                     new AuthenticationProperties
                         {
                         RedirectUri = "",
                         },
                     CookieAuthenticationDefaults.AuthenticationScheme,
                     scheme);
                }
            }
        }
    }

  
