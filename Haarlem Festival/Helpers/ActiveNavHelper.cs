﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Haarlem_Festival.Helpers
{
    public static class ActiveNavHelper
    {
        public static string IsActive(this HtmlHelper html, string controllers = "", string actions = "", string cssClass = "navigation-button-active")
        {
            ViewContext viewContext = html.ViewContext;
            bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

            if (isChildAction)
                viewContext = html.ViewContext.ParentActionViewContext;

            RouteValueDictionary routeValues = viewContext.RouteData.Values;
            string currentAction = routeValues["action"].ToString();
            string currentController = routeValues["controller"].ToString();

            if (String.IsNullOrEmpty(actions))
                actions = currentAction;

            if (String.IsNullOrEmpty(controllers))
                controllers = currentController;

            string[] acceptedActions = actions.Trim().Split(',').Distinct().ToArray();
            string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }
    }
}