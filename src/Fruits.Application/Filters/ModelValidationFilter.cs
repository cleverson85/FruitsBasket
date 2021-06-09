using Fruits.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Fruits.Application.Filters
{
    public class ModelValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new ErrorViewModel(context.ModelState.GetErrorsMessages());
                context.Result = new BadRequestObjectResult(errors);
            }
        }
    }

    public static class ModelStateExtensions
    {
        public static List<string> GetErrorsMessages(this ModelStateDictionary modelState) => 
            modelState
                .SelectMany(c => c.Value.Errors)
                .Select(c => c.ErrorMessage)
                .ToList();       
    }
}
