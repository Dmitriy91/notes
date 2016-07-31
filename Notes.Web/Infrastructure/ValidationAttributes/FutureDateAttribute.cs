using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Notes.Web.Infrastructure.ValidationAttributes
{
    public class FutureDateAttribute : RequiredAttribute, IClientValidatable
    {
        public FutureDateAttribute()
        {
            ErrorMessage = "Only a current date or an ongoing date.";
        }

        public override bool IsValid(object value)
        {
            return base.IsValid(value) && ((DateTime)value) >= DateTime.Parse(DateTime.Now.ToShortDateString());
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            //Not working without this row. Adding an unused parameter is a must.
            rule.ValidationParameters.Add("none", "");
            rule.ValidationType = "futuredate";
            yield return rule;
        }
    }
}
