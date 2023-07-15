using System.ComponentModel.DataAnnotations;

namespace SII_entrega.Entidades
{
    public class Tarjetas
    {
       
       

        [Required]
        public string cvv { get; set; }
        [Required]
        public string numero { get; set; }
        [Required]
        public string vencimiento { get; set; }
        [Required]
        public string nombre { get; set; }
        

    }
}
