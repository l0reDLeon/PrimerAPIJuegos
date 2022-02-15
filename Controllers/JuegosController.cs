using Microsoft.AspNetCore.Mvc;// Es parte de la herencia de abajo, el equivalente a un import en java
using WebAPI1990081.Entidades;
using Microsoft.EntityFrameworkCore;

namespace WebAPI1990081.Controllers
{
    [ApiController] //permite hacer validaciones automáticas respecto a la info recibida aquí
    [Route("api/juegos")]
    public class JuegosController : ControllerBase //heredamos un controlador base
    {

        private readonly ApplicationDbContext dbContext;
        public JuegosController(ApplicationDbContext context) {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Juego>>> Get() {
            return await dbContext.Juegos.Include(x=>x.plataformas).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Juego juego) {
            dbContext.Add(juego);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] //api/juegos/1
        public async Task<ActionResult> Put(Juego juego, int id) {
            if (juego.id != id) {
                return BadRequest("El id del juego no coincide con el establecido en la url.");
            }

            dbContext.Update(juego);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Juegos.AnyAsync(x => x.id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Juego()
            {
                id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
 