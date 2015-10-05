using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;
using MarcBachraty.LocalResource;


namespace MarcBachraty.Models
{
	public class ContactViewModel
	{
		[Required]
		[Display(Name = "firstname", ResourceType = typeof(forms))]  
		public string FirstName { get; set; }

        [Display(Name = "surname", ResourceType = typeof(forms))]  
		public string SurName { get; set; }
		[Display(Name = "city", ResourceType = typeof(forms))]
		public string City { get; set; }
		
		[StringLength(2500, MinimumLength = 1)]
		[DataType(DataType.MultilineText)]
		[Display(Name = "comments", ResourceType = typeof(forms))]
		public string Comments { get; set; }

		[UIHint("_DDRegarding")]
		[Display(Name = "contactreason", ResourceType = typeof(forms))]  
		public string ContactReason { get; set; }

		[Display(Name = "dojo", ResourceType = typeof(forms))]
		public string Dojo { get; set; }
		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "email", ResourceType = typeof(forms))]
		public string Email { get; set; }

		[Display(Name = "phone", ResourceType = typeof(forms))]
		public string Phone { get; set; }

		[Display(Name = "street", ResourceType = typeof(forms))]
		public string Street { get; set; }

		[Display(Name = "website", ResourceType = typeof(forms))]
		public string Website { get; set; }

		[Display(Name = "zip", ResourceType = typeof(forms))]
		public string Zip { get; set; }
		public int NoA { get; set; }
		public int NoB { get; set; }

        [Required]
        public int CaptchaCheck { get; set; }

        public string MessageHeader { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }

    }

    //[Required]
    //[StringLength(200, MinimumLength = 2)]
    //public string Name { get; set; }

    //[Required]
    //[EmailAddress]
    //[DataType(DataType.EmailAddress)]
    //[Display(Name = "Email address")]
    //public string EmailAddress { get; set; }

    //[Required]
    //[StringLength(2500, MinimumLength = 10)]
    //[DataType(DataType.MultilineText)]
    //public string Message { get; set; }
    //}
}