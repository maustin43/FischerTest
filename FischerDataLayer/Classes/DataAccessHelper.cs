using System;
using System.Linq;
using FischerDataLayer.Models;

namespace FischerDataLayer.Classes
{
	//-----------------------------------------------------------------------------------------------------------------------------------------------

	public class DataAccessHelper
	{
		//-------------------------------------------------------------------------------------------------------------------------------------------

		static public company GetCompany(UInt64 companyRecordId)
		{
			using(fischerhomesEntities context = new fischerhomesEntities())
			{
				return (from p in context.companies select p).FirstOrDefault();
			}
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		static public String GetCompanyId(UInt64 companyRecordId)
		{
			using(fischerhomesEntities context = new fischerhomesEntities())
			{
				company c = (from p in context.companies select p).FirstOrDefault();
				if(c == null)
					return String.Empty;
				return c.CompanyId;
			}
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		static public String GetCompanyName(UInt64 companyRecordId)
		{
			using(fischerhomesEntities context = new fischerhomesEntities())
			{
				company c = (from p in context.companies select p).FirstOrDefault();
				if(c == null)
					return String.Empty;
				return c.Description;
			}
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		static public UInt64 GetTableReferenceId(UInt64 companyRecordId, String tableName)
		{
			using(fischerhomesEntities context = new fischerhomesEntities())
			{
				companytablereference tableReference = (from p in context.companytablereferences where p.CompanyRecordId == companyRecordId && p.TableName == tableName select p).FirstOrDefault();
				if(tableReference == null)
					return 0;
				return (UInt64) tableReference.TableCompanyRecordId;
			}
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------
}
