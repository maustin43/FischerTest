using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using FischerDataLayer.Classes;
using FischerDataLayer.Models;
using FischerLib.Logging;

namespace FischerDataLayer.DataObjects
{
	//-----------------------------------------------------------------------------------------------------------------------------------------------

	public class ActivityBase : activity
	{
		UInt64	companyRecordId			= 0;
		LogDb	log						= null;
		UInt64	tableCompanyRecordId	= 0;

		//-------------------------------------------------------------------------------------------------------------------------------------------

		public ActivityBase(UInt64 companyrecordid)
		{
			companyRecordId			= companyrecordid;
			log						= new LogDb(DataAccessHelper.GetCompanyId(companyRecordId));
			tableCompanyRecordId	= DataAccessHelper.GetTableReferenceId(companyRecordId, GetType().BaseType.Name);
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		virtual public Boolean Delete(activity record) 
		{ 
			activity original = null;

			using(fischerhomesEntities context = new fischerhomesEntities())
			{
				try
				{
					if((original = context.activities.Find(record.ActivityRecordId)) == null)
						context.Entry(original).CurrentValues.SetValues(record);

					context.activities.Remove(original); 
					context.SaveChanges(); 
				}
				catch(Exception ex)
				{
					log.WriteError("Delete Activity", ex);

					return false;
				}
			}

			return true;
		} 

		//-------------------------------------------------------------------------------------------------------------------------------------------

		virtual public List<activity> GetList()
		{
			using(fischerhomesEntities context = new fischerhomesEntities())
			{
				return (from p in context.activities where p.CompanyRecordId == tableCompanyRecordId orderby p.ActivityId select p).ToList();
			}
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		virtual public activity GetByRecordId(Decimal recordId)
		{
			using(fischerhomesEntities context = new fischerhomesEntities())
			{
				return (from p in context.activities where p.ActivityRecordId == recordId select p).FirstOrDefault();
			}
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------

		virtual public Boolean Update(activity record)
		{
			using(fischerhomesEntities context = new fischerhomesEntities())
			{
				try
				{
					activity original = GetByRecordId(record.ActivityRecordId);
					if(original == null)
					{
						context.activities.Add(record);
						context.SaveChanges(); 

						return false;
					}

					DbEntityEntry entity  = context.Entry(original);
					if(entity.State != EntityState.Unchanged)
						return false;
					context.Entry(original).CurrentValues.SetValues(record);
					context.SaveChanges(); 
				}
				catch
				{
					return false;
				}
			}
			
			// Return Success
			return true;			
		}

		//-------------------------------------------------------------------------------------------------------------------------------------------
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------
}
