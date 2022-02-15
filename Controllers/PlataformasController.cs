using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI1990081.Entidades;

namespace WebAPI1990081.Controllers
{
    [ApiController]
    [Route("api/plataformas")]
    public class PlataformasController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PlataformasController(ApplicationDbContext context) { 
        
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Plataforma>>> GetAll() {
            return await dbContext.Plataforma.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Plataforma>> GetById(int id) {
            return await dbContext.Plataforma.FirstOrDefaultAsync(x=>x.id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Plataforma plataforma)
        {
            var existeJuego = await dbContext.Juegos.AnyAsync(x=>x.id == plataforma.juegoid);
            if (!existeJuego)
            {
                return BadRequest($"No existe el juego con id:{plataforma.juegoid}");
            }

            dbContext.Add(plataforma);
            await dbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Plataforma plataforma, int id) { 
            var exist = await dbContext.Plataforma.AnyAsync(x=>x.id == id);
            if (!exist) {
                return NotFound("La plataforma especificada no existe");
                
            }

            if (plataforma.id != id) {
                return BadRequest("El id de la plataforma no coincide con la especificada por la url. ");
            }

            dbContext.Update(plataforma);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) {
            var exist = await dbContext.Plataforma.AnyAsync(x => x.id == id);
            if (!exist) {
                return NotFound("El recurso no fue encontrado. ");
            }

            dbContext.Remove(new Plataforma {id = id});
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
