using Microsoft.AspNetCore.Mvc.Filters;
namespace WebAPI1990081.Filtros
{
    public class FiltroDeAccion : IActionFilter
    {
        private readonly ILogger<FiltroDeAccion> log;
        
        public FiltroDeAccion(ILogger<FiltroDeAccion> log)
        {
            this.log = log;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            log.LogInformation("Antes de ejecutar el filtro.");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            log.LogInformation("Después de ejecutar el filtro.");
        }
    }
}
