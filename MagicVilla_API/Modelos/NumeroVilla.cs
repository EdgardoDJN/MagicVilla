using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Modelos
{
    public class NumeroVilla
    {
        //Relaciòn de uno a muchos con la tabla Villa
        //No queremos que el id se genere automaticamente sino que nosotros lo vamos asignando
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNo { get; set; }


        //Para crear una relación con Villa tenemos que crear una propiedad
        //Y luego una navegación
        [Required]
        public int VillaId { get; set; }

        //Navegación
        [ForeignKey("VillaId")]
        public Villa Villa { get; set; }

        public string DetalleEspecial { get; set; }

        public DateTime FechaCreación { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}
