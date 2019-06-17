using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace RootHelpers.AspNetMvc.LayoutManager
{
    public enum Notification
    {
        success,
        error,
        disable,
        warning
    }
    /// <summary>
    /// Aide & accesseur pour les vues
    /// </summary>
    public class LayoutManager
    {
        private readonly System.Web.Mvc.TempDataDictionary _temp = null;


        public string AreaName = null;
        public string ActionName = null;
        public string ControllerName = null;
        public Controller Controller = null;

        public LayoutManager(HtmlHelper html)
        {
            this._temp = html.ViewContext.Controller.TempData;

        }


        /// <summary>
        /// Constructeur complet
        /// </summary>
        /// <param name="controller"></param>
        public LayoutManager(Controller controller)
        {
            this._temp = controller.TempData;
            try
            {
                this.ActionName = controller.RouteData.GetRequiredString("action");
                this.ControllerName = controller.RouteData.GetRequiredString("controller");
                this.Controller = controller;
            }
            catch { }
        }

        public void AlertMsgStay(Notification notification, string Message, string Message2 = null)
        {
            AlertMsgData alert = new AlertMsgData()
            {
                Message = Message,
                Message2 = Message2,
                notification = notification,
            };

            if (_temp != null)
                _temp["AlertMsgStay"] = alert;
        }


        #region RenduHtml

        public string GetAlertMsgHtml(HtmlHelper helper)
        {
            try
            {

                AlertMsgData alert = null;
                if (_temp != null)
                    alert = _temp["AlertMsgStay"] as AlertMsgData;

                if (alert == null)
                    return "";

                _temp["AlertMsgStay"] = null;

                String retour = null;

                if (alert.notification == Notification.success)
                {
                    retour = "<div class='alert alert-success'>";
                    retour += " <div class='notification-text'>";
                    retour += "<p>" +  alert.Message + "<span>" + alert.Message2 + " </span>" + "</p>";
                    retour += "</div>";
                    retour += "<div class='close'>";
                    retour += "</div>";
                    retour += "</div>";
                }
                else if (alert.notification == Notification.warning)
                {
                    retour += "<div class='alert alert-warning'>";
                    retour += " <div class='notification-text'>";
                    retour += "<p>" + alert.Message + "<span>" + alert.Message2 + " </span>" + "</p>";
                    retour += "</div>";
                    retour += "<div class='close'>";
                    retour += "</div>";
                    retour += "</div>";
                }
                else if (alert.notification == Notification.disable)
                {
                    retour += "<div class='alert alert-disable'>";
                    retour += " <div class='notification-text'>";
                    retour += "<p>" + alert.Message + "<span>" + alert.Message2 + " </span>" + "</p>";
                    retour += "</div>";
                    retour += "<div class='close'>";
                    retour += "</div>";
                    retour += "</div>";
                }
                else if (alert.notification == Notification.error)
                {
                    retour += "<div class='alert alert-error'>";
                    retour += " <div class='notification-text'>";
                    retour += "<p>" + alert.Message + "<span>" + alert.Message2 + " </span>" + "</p>";
                    retour += "</div>";
                    retour += "<div class='close'>";
                    retour += "</div>";
                    retour += "</div>";
                }

                return retour.ToString();
            }
            catch (Exception ex)
            {
            }
            return "<div class='alert alert-warning'><b>" + "Ooops" + "</b>" + "incident on the display of notifications" + "</div>";

        }



        #endregion RenduHtml

        private class AlertMsgData
        {
            public string Message { get; set; }
            public string Message2 { get; set; }

            public Notification notification { get; set; }

        }


    }
}