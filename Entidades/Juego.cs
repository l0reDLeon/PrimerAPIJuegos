using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; //para incluir el decorador NotMapped
using WebAPI1990081.Validaciones;

namespace WebAPI1990081.Entidades
{
    public class Juego : IValidatableObject
    {
        public int id { get; set; }

        [Required(ErrorMessage ="El campo nombre es requerido; el campo {0} es requerido")]     //para esto se usa lo de dataanotations y
        [StringLength(maximumLength:20, ErrorMessage ="sobrepasado el limite de caracteres")]   //podemos darle formato con el campo {n}
        [PrimeraLetraMayuscula]      
        public string nombre { get; set; }
        
        [Range(1,100, ErrorMessage ="El campo no entra en el rango de 1 a 100 horas") ]
        [NotMapped] //esto nos deja usar campos que no estén declarados en la base de datos, no los va a guardar pero
                    //nos deja usarlos en cualquier parte del endpoint
        public int horasdejuego { get; set; }

        //ejemplo de credit card
        [CreditCard] //te valida automaticamente que los datos de la tarjeta estén correctos
        [NotMapped]
        public string tarjeta{get;set;}

        
        [Url] //te checa que sea una url válida
        [NotMapped]
        public string url {get;set;}
         
        public List<Plataforma> plataformas { get; set; }

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
         [NotMapped]
        public int menor { get; set; }
        [NotMapped]
        public int mayor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(nombre))
            {
                var primeraLetra = nombre[0].ToString();
                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra de ser mayus", new String[] { nameof(nombre) });
                }
            }

            ////////////////////////////////////
            if(menor > mayor)
            {
                yield return new ValidationResult("El valor de menor no puede ser mas grande que el de mayor", new String[] { nameof(menor) });
            }

        }
    }
}
