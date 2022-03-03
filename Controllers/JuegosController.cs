using Microsoft.AspNetCore.Mvc;// Es parte de la herencia de abajo, el equivalente a un import en java
using WebAPI1990081.Entidades;
using Microsoft.EntityFrameworkCore;

namespace WebAPI1990081.Controllers
{
    [ApiController] //permite hacer validaciones automáticas respecto a la info recibida aquí
    [Route("api/[controller]")] //si ponemos [controller] reemplarazá este nombre por el nombre
                                //del controlador que se declara abajo
    public class JuegosController : ControllerBase //heredamos un controlador base
    {

        private readonly ApplicationDbContext dbContext; 
        public JuegosController(ApplicationDbContext context)
        {                                                       //si cambiamos los nombres del controlador
                                                                //la ruta se modifica auto. por [controller]
            this.dbContext = context;
        }

        [HttpGet]//api/juegos
        [HttpGet("listado")]//api/juegos/listado ("listado")
        [HttpGet("/listado")] // /listado -> localhost:puerto/listado
        public async Task<ActionResult<List<Juego>>> Get() {
            return await dbContext.Juegos.Include(x=>x.plataformas).ToListAsync();
        }

        /*
        //get base sin programación asíoncrona
        public List<Juego> Get()
        {
            return dbContext.Juegos.Include(x => x.plataformas).ToList();
        }

        //get de varios parámetros sin programación asíncrona
        [HttpGet("{id:int}/{param=Halo}")]
        //se puede usar un ActionResult para poder mostrar un NotFound en esta función
        public ActionResult<Juego> GetSinSync(int id, string param)
        {
            var juego = dbContext.Juegos.FirstOrDefault(x => x.id == id);
            if (juego == null) {
                return NotFound("No lo encontré");
            }
            return juego;
        }*/

        //METODO PARA OBTENER EL PRIMER REGISTRO
        [HttpGet("first")] //api/juegos/first
        public async Task<ActionResult<Juego>> PrimerJuego()
        {
            return await dbContext.Juegos.FirstOrDefaultAsync();
        }

        //METODO PARA OBTENER UN REGISTRO POR ID
        [HttpGet("{id:int}")] //pide por id. Si no especificas el tipo puede recibir cualquier cosa,
                              //si especificas que es int entonces solo aceptará int
        public async Task<ActionResult<Juego>> GetByid(int id)
        {
            var juego = await dbContext.Juegos.FirstOrDefaultAsync(x => x.id == id);
            if (juego == null)
            {
                return NotFound();
            }
            return juego;
        }

        //METODO PARA OBTENER UN REGISTRO POR NOMBRE
            //COMENTADO PARA PODER USAR LOS ENDPOINTS DE ABAJO (RUTAS)
        [HttpGet("{nombre}")]
        public async Task<ActionResult<Juego>> GetByNombre(string nombre) 
        {
            var juego = await dbContext.Juegos.FirstOrDefaultAsync(x => x.nombre.Contains(nombre));
            //El método contains manda a llamar a todos los registros que tengan el parámetro que le pasamos
            if (juego == null)
            {
                return NotFound();
            }
            return juego;
        }

        //METODO QUE RECIBE VARIOS PARÁMETROS
        [HttpGet("{id:int}/{param?}")] //al agregar el ? al final,
                                       //hacemos que su implementación sea opcional
        public IActionResult GetSync(int id, string param)
        {
            var juego = dbContext.Juegos.FirstOrDefault(x => x.id == id);
            if (juego == null)
            {
                return NotFound("No se encontró el recurso solicitado");
            }
            return Ok(juego);
        }

        //METODOS QUE ENVIAN VARIABLES A TRAVES DE DISTINTOS MEDIOS

        //METODO QUE ENVIA VARIABLES A TRAVES DE RUTAS
        /*[HttpGet("{name}")]
        public async Task<ActionResult<Juego>> Get([FromRoute] string name) //el dato va a llegar por ruta,
                                                                            //hay otros: body, services, query,[...]
        {
            var juego = await dbContext.Juegos.FirstOrDefaultAsync(x => x.nombre.Contains(name));
            if (juego == null)
            {
                return NotFound();
            }
            return juego;
        }*/

        //METODO QUE ENVIA VARIABLES A TRAVES DEL BODY

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Juego juego)
        {
            dbContext.Add(juego);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        //METODO QUE ENVIA VARIABLES A TRAVES DEL HEADER
        [HttpGet("primero")]
        public async Task<ActionResult<Juego>> PrimerJuego([FromHeader] int valor)
        {
            return await dbContext.Juegos.FirstOrDefaultAsync();
        }

        //METODO QUE USA UN QUERY PARA HACER CONSULTAS (COMO YOUTUBE)
        [HttpGet("query")]
        public async Task<ActionResult<Juego>> Get([FromQuery] string nombre)
        {
            var juego = await dbContext.Juegos.FirstOrDefaultAsync(x => x.nombre.Contains(nombre));
            if (juego == null)
            {
                return NotFound();
            }
            return juego;
        }

                //2 querys
        [HttpGet("query2")]
        public async Task<ActionResult<Juego>> Get([FromQuery] string nombre, [FromQuery] string plataforma)
        {
            var juego = await dbContext.Juegos.FirstOrDefaultAsync(x => x.nombre.Contains(nombre));
            if (juego == null)
            {
                return NotFound();
            }
            return juego;
        }

        ////////////////////////////////////////////////////////////////////////////

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