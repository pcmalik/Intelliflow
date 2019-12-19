using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace WebSvc1.Models
{
    [Validator(typeof(EmployeeValidator))]
    [Serializable]
    [XmlRoot(ElementName = "employee")]
    public class Employee
    {
        [Required]
        [XmlElement(ElementName = "EmployeeNo", DataType = "long")]
        public long EmployeeNo { get; set; }

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "DateOfBirth", DataType = "dateTime")]
        public DateTime DateOfBirth { get; set; }

        [XmlElement(ElementName = "HomeAddress")]
        public string Address { get; set; }

        [XmlElement(ElementName = "Role")]
        public string Role { get; set; }

        [XmlElement(ElementName = "Department")]
        public string Dept { get; set; }
    }

    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.EmployeeNo).NotNull().WithMessage("EmployeeNo cannot be null.");
            RuleFor(x => x.EmployeeNo).GreaterThan(0).WithMessage("EmployeeNo should be a non-zero number.");
        }
    }
}