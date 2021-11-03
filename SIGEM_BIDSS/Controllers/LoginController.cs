// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using SIGEM_BIDSS.TokenStorage;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SIGEM_BIDSS.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignIn()
        {
            if (!Request.IsAuthenticated)
            {
                // Signal OWIN to send an authorization request to Azure
                Request.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            };
        }

        public ActionResult SignOut()
        {
            if (Request.IsAuthenticated)
            {
                var tokenStore = new SessionTokenStore(null,  System.Web.HttpContext.Current, ClaimsPrincipal.Current);
                tokenStore.Clear();
                Request.GetOwinContext().Authentication.SignOut(
                    CookieAuthenticationDefaults.AuthenticationType);

            }

            return View("Index");
        }
    }
}