using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FischerLib.Logging
{
	//-----------------------------------------------------------------------------------------------------------------------------------------------
	
	public class LogWriterSection : ConfigurationSection
	{
		//-------------------------------------------------------------------------------------------------------------------------------------------

		[ConfigurationProperty("applicationName")]
		public String ApplicationName
		{
			get
			{ 
				return (String) this["applicationName"]; 
			}
			set
			{ 
				this["applicationName"] = value; 
			}
		}

		[ConfigurationProperty("emailEnabled")]
		public Boolean EmailEnabled
		{
			get
			{ 
				return (Boolean) this["emailEnabled"]; 
			}
			set
			{ 
				this["emailEnabled"] = value; 
			}
		}

		[ConfigurationProperty("smtpServer")]
		public String SmtpServer
		{
			get
			{ 
				return (String) this["smtpServer"]; 
			}
			set
			{ 
				this["smtpServer"] = value; 
			}
		}

		[ConfigurationProperty("smtpPort")]
		public Int32 SmtpPort
		{
			get
			{ 
				return (Int32) this["smtpPort"]; 
			}
			set
			{ 
				this["smtpPort"] = value; 
			}
		}

		[ConfigurationProperty("smtpUser")]
		public String SmtpUser
		{
			get
			{ 
				return (String) this["smtpUser"]; 
			}
			set
			{ 
				this["smtpUser"] = value; 
			}
		}

		[ConfigurationProperty("smtpPassword")]
		public String SmtpPassword
		{
			get
			{ 
				return (String) this["smtpPassword"]; 
			}
			set
			{ 
				this["smtpPassword"] = value; 
			}
		}

		[ConfigurationProperty("smtpEnableSsl")]
		public Boolean SmtpEnableSsl
		{
			get
			{ 
				return (Boolean) this["smtpEnableSsl"]; 
			}
			set
			{ 
				this["smtpEnableSsl"] = value; 
			}
		}

		[ConfigurationProperty("fromEmailAddress")]
		public String FromEmailAddress
		{
			get
			{ 
				return (String) this["fromEmailAddress"]; 
			}
			set
			{ 
				this["fromEmailAddress"] = value; 
			}
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		[ConfigurationProperty("debugSeverity")]
		public LogWriterSeverityElement DebugSeverity
		{
			get
			{ 
				return (LogWriterSeverityElement)this["debugSeverity"]; }
			set
			{ this["debugSeverity"] = value; }
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		[ConfigurationProperty("errorSeverity")]
		public LogWriterSeverityElement ErrorSeverity
		{
			get
			{ 
				return (LogWriterSeverityElement)this["errorSeverity"]; }
			set
			{ this["errorSeverity"] = value; }
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------
		
		[ConfigurationProperty("informationSeverity")]
		public LogWriterSeverityElement InformationSeverity
		{
			get
			{ 
				return (LogWriterSeverityElement)this["informationSeverity"]; }
			set
			{ this["informationSeverity"] = value; }
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		[ConfigurationProperty("runningSeverity")]
		public LogWriterSeverityElement RunningSeverity
		{
			get
			{ 
				return (LogWriterSeverityElement)this["runningSeverity"]; }
			set
			{ this["runningSeverity"] = value; }
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		[ConfigurationProperty("summarySeverity")]
		public LogWriterSeverityElement SummarySeverity
		{
			get
			{ 
				return (LogWriterSeverityElement)this["summarySeverity"]; }
			set
			{ this["summarySeverity"] = value; }
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		[ConfigurationProperty("transactionSeverity")]
		public LogWriterSeverityElement TransactionSeverity
		{
			get
			{ 
				return (LogWriterSeverityElement)this["transactionSeverity"]; }
			set
			{ this["transactionSeverity"] = value; }
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		[ConfigurationProperty("warningSeverity")]
		public LogWriterSeverityElement WarningSeverity
		{
			get
			{ 
				return (LogWriterSeverityElement)this["warningSeverity"]; }
			set
			{ this["warningSeverity"] = value; }
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------

	public class LogWriterSeverityElement : ConfigurationElement
	{
		[ConfigurationProperty("enabled")]
		public Boolean Enabled
		{
			get
			{
				return (Boolean)this["enabled"];
			}
			set
			{
				this["enabled"] = value;
			}
		}

		[ConfigurationProperty("emailEnabled")]
		public Boolean EmailEnabled
		{
			get
			{
				return (Boolean)this["emailEnabled"];
			}
			set
			{
				this["emailEnabled"] = value;
			}
		}

		[ConfigurationProperty("emailAddress")]
		public String EmailAddress
		{
			get
			{
				return (String)this["emailAddress"];
			}
			set
			{
				this["emailAddress"] = value;
			}
		}

		[ConfigurationProperty("verbose")]
		public Boolean Verbose
		{
			get
			{
				return (Boolean)this["verbose"];
			}
			set
			{
				this["verbose"] = value;
			}
		}
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------
}
