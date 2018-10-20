namespace Feeder.Web.API.Helpers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System;

    public class ValidateObjectResult : ObjectResult
    {
        public ValidateObjectResult(ModelStateDictionary modelState)
            : base(modelState)
        {
            if (modelState == null)
            {
                throw new ArgumentNullException();
            }
            StatusCode = 422;
        }
    }
}
