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

        //Post: /Phonebook/AddPhonebook
        [HttpPost]
        [Route(nameof(AddPhonebook))]
        public async Task<ActionResult<bool>> AddPhonebook([FromBody] PhonebookDTO phonebook)
        {
            try
            {
                return Ok(await iPhonebookService.AddPhonebookAsync(phonebook));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Submitted data: " + JsonSerializer.Serialize(phonebook));
                return BadRequest(false);
            }
        }

        // GET: /Phonebook/GetPhonebooks
        [HttpGet]
        [Route(nameof(GetPhonebooks))]
        public async Task<IEnumerable<PhonebookDTO>> GetPhonebooks()
        {
            try
            {
                return await iPhonebookService.GetPhonebookAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "The application failed to Get()");
                return (IEnumerable<PhonebookDTO>)BadRequest(ex);
            }
        }

        //Post: /Phonebook/AddContact
        [HttpPost]
        [Route(nameof(AddContact))]
        public async Task<ActionResult<bool>> AddContact([FromBody] ContactInfoDTO contactInfo)
        {
            try
            {
                return Ok(await iPhonebookService.AddContactAsync(contactInfo));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Submitted data: " + JsonSerializer.Serialize(contactInfo));
                return BadRequest(false);
            }
        }

        // GET: /Phonebook/GetEntries
        [HttpGet]
        [Route(nameof(GetContacts))]
        public async Task<IEnumerable<ContactInfoDTO>> GetContacts(int phonebookId)
        {
            try
            {
                return await iPhonebookService.GetContactsAsync(phonebookId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "The application failed to GetEntries()");
                return (IEnumerable<ContactInfoDTO>)BadRequest(ex);
            }
        }
    }
}
