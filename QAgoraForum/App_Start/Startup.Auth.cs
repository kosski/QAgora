using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Newtonsoft.Json.Linq;
using Owin;
using System.Security.Claims;

namespace QAgoraForum
{
    public partial class Startup
    {
        // Więcej informacji dotyczących konfigurowania uwierzytelniania można znaleźć na stronie http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Zezwalaj aplikacji na przechowywanie w pliku cookie informacji o zalogowanym użytkowniku
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Anuluj poniższe wiersze, aby włączyć logowanie przy użyciu innych dostawców logowania
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");
            var facebookOptions = new FacebookAuthenticationOptions();
            facebookOptions.AppId = "860154397361256";
            facebookOptions.AppSecret = "6d8fb86bfdc9fd00d615270cb76c8a3c";
            facebookOptions.Scope.Add("email");
            facebookOptions.Provider = new FacebookAuthenticationProvider()
            {
                OnAuthenticated = async facebookContext =>
                {
                    // Save every additional claim we can find in the user
                    foreach (JProperty property in facebookContext.User.Children())
                    {
                        var claimType = string.Format("urn:facebook:{0}", property.Name);
                        string claimValue = (string)property.Value;
                        if (!facebookContext.Identity.HasClaim(claimType, claimValue))
                            facebookContext.Identity.AddClaim(new Claim(claimType, claimValue,
                                  "http://www.w3.org/2001/XMLSchema#string", "External"));
                    }
                }
            };

            app.UseFacebookAuthentication(facebookOptions);

           // app.UseGoogleAuthentication();
        //app.UseGoogleAuthentication(GetGoogleOptions());
        }

        private static GoogleOAuth2AuthenticationOptions GetGoogleOptions()
        {
            return new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "87993309464-pcmfuadnoebh96sfajbk2tk70eu0r58a.apps.googleusercontent.com",
                ClientSecret = "acYcl7wFoAehDFR2jYDtIhYP",
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
                //CallbackPath = new PathString("/Account/ExternalGoogleLoginCallback"),
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                 OnAuthenticated = async googleContext =>
                    {
                        string profileClaimName = string.Format("urn:google:{0}","profile");
                        foreach (JProperty property in googleContext.User.Children())
                            {
                                var claimType = string.Format("urn:google:{0}", property.Name);
                                string claimValue = (string)property.Value;
                                if (!googleContext.Identity.HasClaim(claimType, claimValue))
                                googleContext.Identity.AddClaim(new Claim(claimType, claimValue, 
                                "http://www.w3.org/2001/XMLSchema#string", "External"));
                            }
                    }
                }
            };
        }

    }

}