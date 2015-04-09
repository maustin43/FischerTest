using System;
using System.Configuration;
using System.Web.Http;
using FischerWeb.Code;

namespace FischerExternalWeb.Controllers
{
    public class FischerExternalController : ApiController
    {
		String			decryptedKey	= String.Empty;
		const String	privateKey		= "Private String";

		//-------------------------------------------------------------------------------------------------------------------------------------------

		private Boolean CheckKey(String key)
		{
			// Decypt the Key
			decryptedKey = Encryption.DecryptData(key, true, Convert.ToInt32(ConfigurationManager.AppSettings["KeyElapseSeconds"]));
			if(String.IsNullOrEmpty(decryptedKey) == true)
				return false;

			// Key must start with our Private Key
			if(decryptedKey.StartsWith(privateKey) == false)
				return false;

			return true;
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		[HttpGet]
		public String GetPO(String key, String poNumber)
		{
			if(CheckKey(key) == false)
				return "Invalid Key";

			return "91234567";
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		[HttpGet]
		public String Test()
		{
			return "Test 91234567";
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------
    }
}
