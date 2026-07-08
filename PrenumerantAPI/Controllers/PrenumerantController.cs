using Microsoft.AspNetCore.Mvc;
using PrenumerantAPI.DAL;
using PrenumerantAPI.Models;

namespace PrenumerantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrenumerantController : ControllerBase
    {
        private readonly PrenumerantDAL _dal;

        public PrenumerantController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("PrenumerantDB")!;
            _dal = new PrenumerantDAL(connectionString);
        }

        // GET: api/Prenumerant
        [HttpGet]
        public ActionResult<List<Prenumerant>> HamtaAlla()
        {
            return Ok(_dal.HamtaAlla());
        }

        // GET: api/Prenumerant/5
        [HttpGet("{id}")]
        public ActionResult<Prenumerant> HamtaEn(int id)
        {
            var prenumerant = _dal.HamtaEnPrenumerant(id);

            if (prenumerant == null)
                return NotFound();

            return Ok(prenumerant);
        }

        // POST: api/Prenumerant
        [HttpPost]
        public ActionResult LaggTill(Prenumerant prenumerant)
        {
            _dal.LaggTill(prenumerant);
            return Ok();
        }

        // PUT: api/Prenumerant/5
        [HttpPut("{id}")]
        public ActionResult Uppdatera(int id, Prenumerant prenumerant)
        {
            if (id != prenumerant.PrenumerationsNummer)
                return BadRequest();

            _dal.Uppdatera(prenumerant);
            return NoContent();
        }

        // DELETE: api/Prenumerant/5
        [HttpDelete("{id}")]
        public ActionResult TaBort(int id)
        {
            var prenumerant = _dal.HamtaEnPrenumerant(id);

            if (prenumerant == null)
                return NotFound();

            _dal.TaBort(id);
            return NoContent();
        }
    }
}