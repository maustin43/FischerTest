using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FischerLib.Logging
{
	public class LogDb
	{
		String				companyId	= String.Empty;
		LogWriterSection	config		= null;

		public enum LogWriterSeverity
		{
			Debug,
			Error,
			Information,
			Warning
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		public LogDb(String company)
		{
			companyId	= company;
			config		= (LogWriterSection) ConfigurationManager.GetSection("logWriterGroup/logWriter");
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		Boolean CanLog(LogWriterSeverity severity)
		{
			switch (severity)
			{
				case LogWriterSeverity.Debug:
					return config.DebugSeverity.Enabled;

				case LogWriterSeverity.Error:
					return config.ErrorSeverity.Enabled;

				case LogWriterSeverity.Information:
					return config.InformationSeverity.Enabled;

				case LogWriterSeverity.Warning:
					return config.WarningSeverity.Enabled;

				default:
					return false;
			}
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		void Email(String messageBody, LogWriterSeverity severity)
		{
			String ToAddress = string.Empty;

			if (config.EmailEnabled == false)
				return;

			switch (severity)
			{
				case LogWriterSeverity.Debug:
					if (config.DebugSeverity.EmailEnabled == false)
						return;
					ToAddress = config.DebugSeverity.EmailAddress;
					break;
				case LogWriterSeverity.Error:
					if (config.ErrorSeverity.EmailEnabled == false)
						return;
					ToAddress = config.ErrorSeverity.EmailAddress;
					break;
				case LogWriterSeverity.Information:
					if (config.InformationSeverity.EmailEnabled == false)
						return;
					ToAddress = config.InformationSeverity.EmailAddress;
					break;
				case LogWriterSeverity.Warning:
					if (config.WarningSeverity.EmailEnabled == false)
						return;
					ToAddress = config.WarningSeverity.EmailAddress;
					break;
				default:
					if (config.InformationSeverity.EmailEnabled == false)
						return;
					ToAddress = config.InformationSeverity.EmailAddress;
					break;
			}

			// Send Message
			MailMessage Message = new MailMessage();
			Message.From = new MailAddress(config.FromEmailAddress);
			Message.To.Add(new MailAddress(ToAddress));
			Message.Subject = string.Format("{0} - {1} Message", config.ApplicationName, severity.ToString());
			Message.Body = messageBody;
			try
			{
				SmtpClient Smtp = new SmtpClient();
				Smtp.Credentials = new System.Net.NetworkCredential(config.SmtpUser, config.SmtpPassword);
				Smtp.EnableSsl = config.SmtpEnableSsl;
				Smtp.Host = config.SmtpServer;
				Smtp.Port = config.SmtpPort;

				Smtp.Send(Message);
			}
			catch (Exception)
			{
				return;
			}
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		public void WriteDebug(String message, Exception ex = null, Object sourceObject = null, String callerName = "", String callerFile = "", Int32 callerLine = 0, String server = "", String userName = "", LogWriterSeverity severity = LogWriterSeverity.Debug, [CallerMemberName] String memberName = "", [CallerFilePath] String memberFile = "", [CallerLineNumber] int memberLine = 0) { Write(message, ex, sourceObject, callerName, callerFile, callerLine, server, userName, severity, memberName, memberFile, memberLine); }
		public void WriteError(String message, Exception ex = null, Object sourceObject = null, String callerName = "", String callerFile = "", Int32 callerLine = 0, String server = "", String userName = "", LogWriterSeverity severity = LogWriterSeverity.Error, [CallerMemberName] String memberName = "", [CallerFilePath] String memberFile = "", [CallerLineNumber] int memberLine = 0) { Write(message, ex, sourceObject, callerName, callerFile, callerLine, server, userName, severity, memberName, memberFile, memberLine); }
		public void WriteInformation(String message, Exception ex = null, Object sourceObject = null, String callerName = "", String callerFile = "", Int32 callerLine = 0, String server = "", String userName = "", LogWriterSeverity severity = LogWriterSeverity.Information, [CallerMemberName] String memberName = "", [CallerFilePath] String memberFile = "", [CallerLineNumber] int memberLine = 0) { Write(message, ex, sourceObject, callerName, callerFile, callerLine, server, userName, severity, memberName, memberFile, memberLine); }
		public void WriteWarning(String message, Exception ex = null, Object sourceObject = null, String callerName = "", String callerFile = "", Int32 callerLine = 0, String server = "", String userName = "", LogWriterSeverity severity = LogWriterSeverity.Warning, [CallerMemberName] String memberName = "", [CallerFilePath] String memberFile = "", [CallerLineNumber] int memberLine = 0) { Write(message, ex, sourceObject, callerName, callerFile, callerLine, server, userName, severity, memberName, memberFile, memberLine); }

		//-------------------------------------------------------------------------------------------------------------------------------------------

		void Write(String message, Exception exception, Object sourceObject, String callerName, String callerFile, Int32 callerLine, String server, String userName, LogWriterSeverity severity, String memberName, String memberFile, int memberLine)
		{
			if (CanLog(severity) == false)
				return;

			log log = new log();
			log.ApplicationName		= config.ApplicationName;
			log.CallerFile			= callerFile;
			log.CallerMethodLine	= callerLine;
			log.CallerMethodName	= callerName;
			log.CompanyId			= companyId;
			log.Exception			= exception != null ? WriteException(exception) : String.Empty;
			log.ExceptionStackTrace = exception != null ? exception.StackTrace : String.Empty;
			log.LogCallerFile		= memberFile;
			log.LogCallerLine		= memberLine;
			log.LogCallerMethodName = memberName;
			log.LogDate				= DateTime.Now;
			log.LogRecordId			= 0;
			log.LogServer			= server;
			log.LogUser				= userName;
			log.Message				= message;
			log.MessageType			= (int)severity;
			log.ObjectData			= WriteObjectData(sourceObject);
			log.ObjectType			= sourceObject != null ? sourceObject.GetType().Name : String.Empty;

			using (fischerloggingEntities context = new fischerloggingEntities())
			{
				try
				{
					context.logs.Add(log);
					context.SaveChanges();
				}
				catch
				{
				}
			}

			if (config.EmailEnabled == true)
			{
				//Email(String.Format("Message: {0}, LogId:{1}", message, log.LogId.ToString()), severity);
			}
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		String WriteDataRow(DataRow dataRow)
		{
			StringBuilder messageBody = new StringBuilder();

			foreach (DataColumn column in dataRow.Table.Columns)
			{
				try { messageBody.AppendLine(String.Format("{0, 50} = {1}", column.ColumnName, dataRow[column.ColumnName])); }
				catch (Exception) { }
			}

			return messageBody.ToString();
		}

		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		String WriteException(Exception ex)
		{
			StringBuilder messageBody = new StringBuilder();

			messageBody.AppendFormat("Exception: {0}\r\n", ex.Message);
			if (ex.InnerException != null)
				messageBody.AppendFormat("Inner Exception: {0}", ex.InnerException.Message);

			return messageBody.ToString();
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		String WriteProperties(Object sourceObject)
		{
			StringBuilder messageBody = new StringBuilder();

			try
			{
				var properties = sourceObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
				foreach (PropertyInfo prop in properties)
				{
					try { messageBody.AppendLine(String.Format("{0, 50} = {1}", prop.Name, prop.GetValue(sourceObject, null))); }
					catch { }
				}
			}
			catch
			{
			}

			return messageBody.ToString();
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		String WriteObjectData(Object sourceObject)
		{
			if (sourceObject == null)
				return String.Empty;

			var a = sourceObject.GetType();

			if (sourceObject.GetType().BaseType == typeof(DataRow))
			{
				return WriteDataRow((DataRow)sourceObject);
			}

			return WriteProperties(sourceObject);
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------
	}
}
