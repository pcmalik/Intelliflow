using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace WebSvc1.Models
{
    [Validator(typeof(BonusRecipientsValidator))]
    [Serializable]
    [XmlRoot(ElementName = "BonusRecipients")]

    public class BonusRecipients
    {
        [XmlElement(ElementName = "BonusAmount", DataType = "decimal")]
        public decimal BonusAmount { get; set; }

        [Required]
        [MinLength(1)]
        [XmlElement(ElementName = "Recipients")]
        public Employee[] Recipients { get; set; }
    }

    public class BonusRecipientsValidator : AbstractValidator<BonusRecipients>
    {
        public BonusRecipientsValidator()
        {
            RuleFor(x => x.BonusAmount).GreaterThan(0).WithMessage("Bonus should be a non-zero value.");
        }
    }

}