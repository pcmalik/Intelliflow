using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebApp1.DataContracts;
using WebApp1.Models;
using System.Text;

namespace WebApp1.Controllers
{
    public class BonusController : Controller
    {
        // GET: Bonus
        public ActionResult Index()
        {
            return View("View");
        }

        // POST: Bonus/Allocate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Allocate(BonusViewModel bonusAllocation)
        {
            try
            {
                //commonet by pmalik: ConfigureAwait(false) is used so that next line after async call should go to next 
                //available thread from threadpool instead of waiting for the original thread to become free
                var employees = await GetEmployees().ConfigureAwait(false);
                List<Employee> recipients = new List<Employee>();

                for (int i = 0; i < employees.Count; i++)
                {
                    if (i % bonusAllocation.OneInXEmployees == 0)
                    {
                        var recipient = employees[i];
                        recipients.Add(recipient);

                        /*comment by pmalik:
                          Removing recipient from employees list was reducing its employees count 
                          and hence the money assigned to pay out for bonuses was not all being paid out
                        */
                        //employees.Remove(recipient); 
                    }
                }

                var bonusRecipients = new BonusRecipients()
                {
                    BonusAmount = bonusAllocation.Amount,
                    Recipients = recipients
                };

                AllocateBonus(bonusRecipients);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("View");
            }
        }

        private static void AllocateBonus(BonusRecipients bonusRecipients)
        {
            HttpClient client = GetHttpClient();

            StringContent content = new StringContent(JsonConvert.SerializeObject(bonusRecipients), Encoding.UTF8, "application/json");

            client.PostAsync("api/Remuneration", content);
        }

        private async Task<List<Employee>> GetEmployees()
        {
            HttpClient client = GetHttpClient();

            HttpResponseMessage response = await client.GetAsync($"api/employee");
            if (response.IsSuccessStatusCode)
            {
                string employeeData = await response.Content.ReadAsStringAsync();
                List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(employeeData);

                return employees;
            }

            return new List<Employee>();
        }

        private static HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57652/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

    }
}