using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Employee.Web.Validation
{
    public class RemoteClientSideValidation : RemoteAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Type controller = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(type => type.Name.ToLower() == string.Format("{0}Controller", this.RouteData["contoller"].ToString()).ToLower());

            if (controller != null)
            {
                MethodInfo action = controller.GetMethods().FirstOrDefault(method => method.Name.ToLower() == this.RouteData["action"].ToString().ToLower());
                if (action != null)
                {
                    object instance = Activator.CreateInstance(controller);
                    object response = action.Invoke(instance, new object[] { value });

                    if (response is JsonResult)
                    {
                        //object jsonData = ((JsonResult)response).Data;

                        object jsonData = ((JsonResult)response);
                        if (jsonData is bool boolean)
                        {
                            return boolean ? ValidationResult.Success : new ValidationResult(this.ErrorMessage);
                        }
                    }
                }
            }

            return new ValidationResult(this.ErrorMessage);
            //return base.IsValid(value, validationContext);
        }

        public RemoteClientSideValidation(string routeName) : base(routeName)
        {
        }

        public RemoteClientSideValidation(string action, string controller) : base(action, controller)
        {

        }

        public RemoteClientSideValidation(string action, string controller, string areaName) : base(action, controller, areaName)
        {

        }
    }


}
