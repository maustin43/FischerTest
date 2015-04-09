using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using FischerWeb.Code;

namespace FischerWeb.Controllers
{
    public class FischerWebAPIController : ApiController
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

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://Ron-PC");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = client.GetAsync(String.Format("FischerExternalWeb/api/fischerExternal/GetPO?key={0}&ponumber={1}", key, poNumber)).Result;
				//HttpResponseMessage response = client.GetAsync("FischerExternalWeb/api/fischerexternal/Test").Result;
				if (response.IsSuccessStatusCode)
				{
					return response.Content.ReadAsStringAsync().Result;
				}
			}

			return "";
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------
		
		[HttpGet]
		public String Login(String user, String password)
		{
			// Perform Authentication

			// Encrypt the User Name along with our Private Key
			return Encryption.EncryptData(privateKey + user, true);
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		[HttpGet]
		public String ValidateKey(String key)
		{
			if(CheckKey(key) == false)
				return "Invalid Key";

			// Extract User Name
			String userName = decryptedKey.Substring(privateKey.Length);

			return "Valid Key";
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------
    }
}
