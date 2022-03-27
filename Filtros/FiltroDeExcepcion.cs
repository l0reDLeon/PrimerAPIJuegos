using Microsoft.AspNetCore.Mvc.Filters;
namespace WebAPI1990081.Filtros
{
    public class FiltroDeExcepcion :  ExceptionFilterAttribute
    {
        private readonly ILogger<FiltroDeExcepcion> log;

        public FiltroDeExcepcion(ILogger<FiltroDeExcepcion> logger)
        {
            this.log = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            log.LogError(context.Exception, context.Exception.Message);
            
            base.OnException(context);  
        }
    }
}
