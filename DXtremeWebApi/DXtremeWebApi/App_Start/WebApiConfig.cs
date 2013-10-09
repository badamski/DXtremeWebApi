using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DXtremeWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                         name: "DefaultApi",
                         routeTemplate: "api/{controller}/{id}",
                         defaults: new { id = RouteParameter.Optional }
                     );

            config.Routes.MapHttpRoute(
                name: "Logging",
                routeTemplate: "api/{controller}/auth/{username}/{password}",
                defaults: new { username = RouteParameter.Optional, password = RouteParameter.Optional, Action = "UserAuth" }
            );

            config.Routes.MapHttpRoute(
                name: "SendMessage",
                routeTemplate: "api/{controller}/send/{idfrom}/{idto}/{msg}",
                defaults: new { idfrom = RouteParameter.Optional, idto = RouteParameter.Optional, msg = RouteParameter.Optional, Action = "SendMessage" }
            );

            config.Routes.MapHttpRoute(
                name: "MessageDetails",
                routeTemplate: "api/{controller}/{userId}/{msgId}/details/",
                defaults: new { userId = RouteParameter.Optional, msgId = RouteParameter.Optional, Action = "GetMessageDetails" }
            );

            config.Routes.MapHttpRoute(
                name: "GetUserTimeline",
                routeTemplate: "api/{controller}/{userId}/timeline/{filter}",
                defaults: new { userId = RouteParameter.Optional, filter = RouteParameter.Optional, Action = "GetUserTimeline" }
            );

            config.Routes.MapHttpRoute(
                name: "GetUserMessages",
                routeTemplate: "api/{controller}/user/{userId}",
                defaults: new { userId = RouteParameter.Optional, Action = "GetUserMessages" }
            );

            config.Routes.MapHttpRoute(
                name: "GetUserDocuments",
                routeTemplate: "api/{controller}/user/{userId}/documents",
                defaults: new { userId = RouteParameter.Optional, Action = "GetUserDocuments" }
            );

            config.Routes.MapHttpRoute(
                name: "GetUserInboxMessages",
                routeTemplate: "api/{controller}/user/{userId}/inbox",
                defaults: new { userId = RouteParameter.Optional, Action = "GetUserInboxMessages" }
            );

            config.Routes.MapHttpRoute(
                name: "GetUserSentMessages",
                routeTemplate: "api/{controller}/user/{userId}/sent",
                defaults: new { userId = RouteParameter.Optional, Action = "GetUserSentMessages" }
            );

            config.Routes.MapHttpRoute(
               name: "GetByName",
               routeTemplate: "api/{controller}/getbyname/{searchstring}",
               defaults: new { searchstring = RouteParameter.Optional, Action = "GetByName" }
            );

            config.Routes.MapHttpRoute(
                name: "ShareDocument",
                routeTemplate: "api/{controller}/{docId}/sharedocument/{userId1}/{userId2}/{userId3}",
                defaults: new { docId = RouteParameter.Optional, userId1 = RouteParameter.Optional, userId2 = RouteParameter.Optional, userId3 = RouteParameter.Optional, Action = "ShareDocument" }
            );
        }
    }
}
