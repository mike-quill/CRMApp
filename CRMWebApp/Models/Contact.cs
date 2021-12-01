using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
	public class Contact
	{
        public Contact()
		{
            ContactCategories = new HashSet<ContactCategory>();
		}

        public int ID { get; set; }

        [Display(Name = "Contact")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "Cell Phone")]
        public string CellPhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(CellPhone))
                {
                    return "";
                }
                else
                {
                    return "(" + CellPhone.Substring(0, 3) + ") " + CellPhone.Substring(3, 3) + "-" + CellPhone.Substring(6, 4);
                }
            }
        }

        [Display(Name = "Work Phone")]
        public string WorkPhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(WorkPhone))
                {
                    return "";
                }
                else
                {
                    return "(" + WorkPhone.Substring(0, 3) + ") " + WorkPhone.Substring(3, 3) + "-" + WorkPhone.Substring(6, 4);
                }
            }
        }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the First Name blank.")]
        [StringLength(50, ErrorMessage = "First Name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the Last Name blank.")]
        [StringLength(50, ErrorMessage = "Last Name cannot be more than 50 characters long.")]
        public string LastName { get; set; }

        [Display(Name = "Job Title")]
        [StringLength(50, ErrorMessage = "Job Title cannot be more than 50 characters long.")]
        public string JobTitle { get; set; }

        [Display(Name = "Cell Phone #")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string CellPhone { get; set; }

        [Display(Name = "Work Phone #")]
        [RegularExpression("^(\\d{10}$)?", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string WorkPhone { get; set; }

        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Active { get; set; }

        [StringLength(500, ErrorMessage = "Contact Notes cannot be more than 500 characters long.")]
        public string Notes { get; set; }

        [Display(Name = "Company")]
        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public virtual ICollection<ContactCategory> ContactCategories { get; set; }
    }
}
