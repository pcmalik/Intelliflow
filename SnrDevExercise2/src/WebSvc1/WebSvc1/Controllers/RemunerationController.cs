using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebSvc1.Models;
using WebSvc1.Repositories;

namespace WebSvc1.Controllers
{
    public class RemunerationController : ApiController
    {
        private readonly IBonusRepository _bonusRepository;

        public RemunerationController(IBonusRepository bonusRepository)
        {
            if (bonusRepository == null)
                throw new ArgumentNullException("bonusRepository");

            _bonusRepository = bonusRepository;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(BonusRecipients bonusRecipients)
        {
            try
            {
                if (bonusRecipients == null)
                    return BadRequest("Bonus recipients data is not initilised");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var employeeBonusList = new List<EmployeeBonus>();

                bonusRecipients?.Recipients?.ToList()
                    .ForEach(x => employeeBonusList.Add(new EmployeeBonus
                    {
                        EmployeeNo = x.EmployeeNo,
                        BonusDate = DateTime.Now,
                        BonusAmount = bonusRecipients.BonusAmount
                    }));

                if (await _bonusRepository.SaveAsync(employeeBonusList).ConfigureAwait(false))
                    return StatusCode(HttpStatusCode.Accepted);
                else
                    return BadRequest("Failed to save bonus recipients data");
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }

        }
    }
}
