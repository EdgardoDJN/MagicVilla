using MagicVilla_API.Modelos.Dto;

namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDto> VillaList = new List<VillaDto>
        {
            new VillaDto
            {
                Id = 1,
                Nombre = "Villa 1",
                Ocupantes = 4,
                MetrosCuadrados = 100
            },
            new VillaDto
            {
                Id = 2,
                Nombre = "Villa 2",
                Ocupantes = 6,
                MetrosCuadrados = 150
            },
            new VillaDto
            {
                Id = 3,
                Nombre = "Villa 3",
                Ocupantes = 8,
                MetrosCuadrados = 200
            }
        };
    }
}
