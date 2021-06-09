using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Phonebook.BLL;
using Phonebook.DAL.Models.DTO;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Phonebook.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhonebookController : ControllerBase
    {
        private readonly IPhonebookService iPhonebookService;
        public PhonebookController(IPhonebookService iPhoneService)
        {
            iPhonebookService = iPhoneService;
        }

        //Post: /Phonebook/AddEntry
        [HttpPost()]
        [Route(nameof(AddEntry))]
        public async Task<ActionResult<bool>> AddEntry([FromBody] ContactInfoDTO contactInfo)
        {
            try
            {
                return Ok(await iPhonebookService.Save(contactInfo));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Submitted data: " + JsonSerializer.Serialize(contactInfo));
                return BadRequest(false);
            }
        }

        // GET: /Phonebook/GetEntries
        [HttpGet]
        [Route(nameof(GetEntries))]
        public async Task<IEnumerable<ContactInfoDTO>> GetEntries(int phonebookId)
        {
            try
            {
                return await iPhonebookService.GetEntriesAsync(phonebookId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "The application failed to GetEntries()");
                return (IEnumerable<ContactInfoDTO>)BadRequest(ex);
            }
        }
    }
}
