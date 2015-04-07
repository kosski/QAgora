using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using System.Security.Claims;

namespace QAgora
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

            app.UseFacebookAuthentication(
               appId: "860154397361256",
               appSecret: "6d8fb86bfdc9fd00d615270cb76c8a3c");

           // app.UseGoogleAuthentication();
            var googleOAuth2AuthenticationOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "87993309464-pcmfuadnoebh96sfajbk2tk70eu0r58a.apps.googleusercontent.com",
                ClientSecret = "acYcl7wFoAehDFR2jYDtIhYP",
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
                CallbackPath = new PathString("/Account/ExternalGoogleLoginCallback"),
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = async context =>
                    {
                        context.Identity.AddClaim(new Claim("picture", context.User.GetValue("picture").ToString()));
                        context.Identity.AddClaim(new Claim("profile", context.User.GetValue("profile").ToString()));
                        context.Identity.AddClaim(new Claim("Login", context.User.GetValue("profile").ToString()));
                    }
                }
            };

            googleOAuth2AuthenticationOptions.Scope.Add("email");

            app.UseGoogleAuthentication(googleOAuth2AuthenticationOptions);
        }
    }
}