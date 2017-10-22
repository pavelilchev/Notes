namespace SimpleMvc.Framework.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Attributes.Property;
    using Contracts;
    using Models;
    using Security;
    using ViewEngine;
    using Views;
    using WebServer.Http;
    using WebServer.Http.Contracts;
    using System.ComponentModel.DataAnnotations;

    public abstract class Controller
    {
        protected Controller()
        {
            this.Model = new ViewModel();
            this.User = new Authentication();
        }

        protected ViewModel Model { get; }

        protected internal IHttpRequest Request { get; internal set; }

        protected internal Authentication User { get; private set; }

        protected internal void InitializeController()
        {
            var user = this.Request
                .Session
                .Get<string>(SessionStore.CurrentUserKey);

            if (user != null)
            {
                this.User = new Authentication(user);
            }
        }

        protected void SignIn(string name)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, name);
        }

        protected void SignOut()
        {
            this.Request.Session.Clear();
        }

        protected bool IsValidModel(object bindingModel)
        {
            foreach (var property in bindingModel.GetType().GetProperties())
            {
                IEnumerable<Attribute> attributes =
                    property.GetCustomAttributes()
                        .Where(a => a is ValidationAttribute);

                if (!attributes.Any())
                {
                    continue;
                }

                foreach (ValidationAttribute attribute in attributes)
                {
                    if (!attribute.IsValid(property.GetValue(bindingModel)))
                    {
                        Model.Errors.Add(attribute.ErrorMessage);
                    }
                }
            }

            return Model.Errors.Count == 0;
        }

        private void InitializeViewModelData()
        {
            this.Model["displayTypeAuthenticated"] = this.User.IsAuthenticated ? "flex" : "none";
            this.Model["displayTypeNotAuthenticated"] = this.User.IsAuthenticated ? "none" : "flex";
            this.Model["username"] = this.User.Name;

            if (!this.Model.Errors.Any())
            {
                this.Model.Data["showError"] = "none";
            }
            else
            {
                Model.Data["showError"] = "block";
                Model.Data["errors"] = string.Join("", Model.Errors.Select(e => $"<li>{e}</li>"));
            }
        }

        protected IViewable View([CallerMemberName] string caller = "")
        {
            this.InitializeViewModelData();

            string controllerName = this.GetType()
                .Name
                .Replace(MvcContext.Get.ControllersSuffix, string.Empty);

            string fullQualifiedName = string.Format(
                "{0}\\{1}\\{2}",
                MvcContext.Get.ViewsFolder,
                controllerName,
                caller);

            IRenderable view = new View(fullQualifiedName, this.Model.Data);

            return new ViewResult(view);
        }

        protected IRedirectable RedirectToAction(string redirectUrl)
        {
            return new RedirectResult(redirectUrl);
        }
    }
}