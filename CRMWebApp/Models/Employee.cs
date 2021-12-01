using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
    public class Employee
    {

        public Employee()
        {
            Active = true;
        }

        public int ID { get; set; }

        [Display(Name = "Employee")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string FormalName
        {
            get
            {
                return LastName + ", " + FirstName;
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

        [Display(Name = "Home Phone")]
        public string HomePhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(HomePhone))
                {
                    return "";
                }
                else
                {
                    return "(" + HomePhone.Substring(0, 3) + ") " + HomePhone.Substring(3, 3) + "-" + HomePhone.Substring(6, 4);
                }
            }
        }

        [Display(Name =" Emergency Phone")]
        public string EmergencyContactPhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(EmergencyContactPhone))
                {
                    return "";
                }
                else
                {
                    return "(" + EmergencyContactPhone.Substring(0, 3) + ") " + EmergencyContactPhone.Substring(3, 3) + "-" + EmergencyContactPhone.Substring(6, 4);
                }
            }
        }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the First Name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the Last Name blank.")]
        [StringLength(50, ErrorMessage = "Last name cannot be more than 50 characters long.")]
        public string LastName { get; set; }

        [Display(Name = "Address Line 1")]
        //[Required(ErrorMessage = "You cannot leave the Address Line 1 blank.")]
        [StringLength(100, ErrorMessage = "Address Line 1 cannot be more than 100 characters long.")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        [StringLength(100, ErrorMessage = "Address Line 2 cannot be more than 100 characters long.")]
        public string AddressLine2 { get; set; }

        [Display(Name = "City ")]
        [StringLength(100, ErrorMessage = "City cannot be more than 100 characters long.")]
        public string City { get; set; }

        [Display(Name = "Postal Code")]
        [RegularExpression("^(?!.*[DFIOQU])[A-VXY][0-9][A-Z] ?[0-9][A-Z][0-9]$", ErrorMessage = "Please enter a valid Postal Code in the A1A 1A1 format.")]
        public string PostalCode { get; set; }

        [Display(Name = "Cell Phone #")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string CellPhone { get; set; }

        [Display(Name = "Home Phone #")]
        [RegularExpression("^(\\d{10}$)?", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string HomePhone { get; set; }

        //[Required(ErrorMessage = "Email Address is required.")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Date of Birth")]
        //[Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }

        //[Required(ErrorMessage = "You must enter the wage amount.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(9,2)")]//So only 5 bytes to store in SQL Server
        [Range(0, 9999999.99, ErrorMessage = "Invalid wage.")]
        public decimal? Wage { get; set; }

        //[Required(ErrorMessage = "You must enter the expense amount.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(9,2)")]//So only 5 bytes to store in SQL Server
        [Range(0, 9999999.99, ErrorMessage = "Invalid expense.")]
        public decimal? Expense { get; set; }

        [Display(Name = "Date Joined")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateJoined { get; set; }

        [Display(Name = "Date Inactive")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateInactive { get; set; }

        [Display(Name = "Key Fob")]
        [RegularExpression("^((\\d){4}:(\\d){5})?$", ErrorMessage = "Key Fob must be in 0000:00000 format.")]
        public string KeyFob { get; set; }

        public bool Active { get; set; }

        [Display(Name ="Account Created")]
        public bool IsUser { get; set; }

        [Display(Name = "Emergency Contact Name")]
        //[Required(ErrorMessage = "You cannot leave the Emergency Contact Name blank.")]
        [StringLength(100, ErrorMessage = "Emergency Contact Name cannot be more than 100 characters long.")]
        public string EmergencyContactName { get; set; }

        [Display(Name = "Emergency Contact Phone #")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string EmergencyContactPhone { get; set; }

        [StringLength(500, ErrorMessage = "Employee Notes cannot be more than 500 characters long.")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Display(Name = "Province/State")]
        public int? ProvinceID { get; set; }
        [Display(Name = "Province/State")]
        public virtual Province Province { get; set; }

        [Display(Name = "Country")]
        public int? CountryID { get; set; }
        public virtual Country Country { get; set; }

        [Display(Name = "Job Position")]
        public int? JobPositionID { get; set; }
        [Display(Name = "Job Position")]
        public virtual JobPosition JobPosition { get; set; }

        [Display(Name = "Employment Type")]
        public int? EmploymentTypeID { get; set; }
        [Display(Name = "Employment Type")]
        public virtual EmploymentType EmploymentType { get; set; }
    }
}
