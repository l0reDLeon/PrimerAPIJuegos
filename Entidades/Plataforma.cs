namespace WebAPI1990081.Entidades
{
    public class Plataforma
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string compania { get; set; }

        public int juegoid { set; get; }
        public Juego juego { set; get; }
    }
}
