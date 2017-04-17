using fidoBackend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace fidoBackend.Services
{
    public class AccountingServices : BaseService
    {

        public async static Task<Status> AddLedger(Ledgers ledger)
        {
            try
            {
                if (ledger.id == null)
                    await MobileService.GetTable<Ledgers>().InsertAsync(ledger);
                else
                    await MobileService.GetTable<Ledgers>().UpdateAsync(ledger);
                return new Models.Status() { result = true, message = "Successfully Added" };
            }
            catch (Exception e)
            {
                return new Models.Status() { result = false, message = e.ToString() };
            }
        }
        public static async Task<Status> AddJorunal(Accounts account)
        {
            try
            {
                if(account.id==null)
                { 
                    await MobileService.GetTable<Accounts>().InsertAsync(account);
                }
                else
                {
                    await MobileService.GetTable<Accounts>().UpdateAsync(account);
                }
            }

                return new Models.Status() { result = true, message = "Successfully Added" };
            }
            catch (Exception e)
            {
                return new Models.Status() { result = false, message = e.ToString() };
            }
        }

        public static async Task<Status> ListLedgers(string organisationId)
        {
            try
            {
                var s = await MobileService.GetTable<Ledgers>().Where(x => x.organisation == organisationId).ToListAsync();
                return new Models.Status() { result = true, data = s, message = "Successfully Added" };
            }
            catch (Exception e)
            {
                return new Models.Status() { result = false, message = e.ToString() };
            }
        }
        public static async Task<Status> ListJournals(string organisationId, DateTimeOffset start, DateTimeOffset end)
        {
            end = end.AddDays(1);
            try
            {
                var s = await MobileService.GetTable<Accounts>().Where(x => x.organisation == organisationId).ToListAsync();
                var d = s.Where(x => x.date >= start.ToUniversalTime() && x.date.ToUniversalTime() <= end).ToList();
                return new Models.Status() { result = true, data = d, message = "Successfully Added" };
            }
            catch (Exception e)
            {
                return new Models.Status() { result = false, message = e.ToString() };
            }
        }


    }
}
