using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarcBachraty.Classes
{
	public enum Status
	{
		Ok = 0,
		Saved = 1,
		Deleted = 2,
		Updated = 3,
		ModelStateNotValid = 4,
		Exception
	}
	public class CallResponse
	{
		public string MessageHeader { get; set; }
		public string Message { get; set; }
		public string Sender { get; set; }
		public string Receiver { get; set; }
		public string Status { get; set; }
	}

    
}