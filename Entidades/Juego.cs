using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; //para incluir el decorador NotMapped

namespace WebAPI1990081.Entidades
{
    public class Juego
    {
        public int id { get; set; }

        [Required(ErrorMessage ="El campo nombre es requerido; el campo {0} es requerido")]     //para esto se usa lo de dataanotations y
        [StringLength(maximumLength:20, ErrorMessage ="sobrepasado el limite de caracteres")]   //podemos darle formato con el campo {n}
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
    }
}
