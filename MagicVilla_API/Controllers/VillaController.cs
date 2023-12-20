using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db ;
        private readonly IMapper _mapper;
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        //De la interfaz IActionResult para cualquier tipo de retorno
        //Lo volvemos asincrono para que no se bloquee el hilo
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obteniendo villas");
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            //200
            //return Ok(VillaStore.VillaList);
            //Es como si hicieramos un selec * from villas
            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            //Agregar validación de existencia de la villa
            if (id == 0)
            {
                _logger.LogError("El id de la villa no puede ser 0");
                return BadRequest("El id de la villa no puede ser 0");//400
            }
            //var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);

            if (villa == null)
                return NotFound($"La villa con id {id} no existe");//404

            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto villaCreateDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);//400         
            }

            if(await _db.Villas.FirstOrDefaultAsync(v => v.Nombre.ToLower() == villaCreateDto.Nombre.ToLower()) !=null)
            {
                ModelState.AddModelError("Nombre", "El nombre de la villa ya existe");
                return BadRequest(ModelState);//400
            }
            //Agregar validación de existencia de la villa
            if (villaCreateDto == null)
                return BadRequest("La villa no puede ser nula");//400

            //if(villaDto.Id>0)
                //return StatusCode(StatusCodes.Status500InternalServerError, "El id de la villa no puede ser mayor a 0");//500

            //villaDto.Id = _db.Villas.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            //VillaStore.VillaList.Add(villaDto);

            /*Villa modelo = new()
            {
                Nombre = villaCreateDto.Nombre,
                Detalle = villaCreateDto.Detalle,
                Tarifa = villaCreateDto.Tarifa,
                Ocupantes = villaCreateDto.Ocupantes,
                MetrosCuadrados = villaCreateDto.MetrosCuadrados,
                ImagenUrl = villaCreateDto.ImagenUrl,
                Amenidad = villaCreateDto.Amenidad
            };
            */
            Villa modelo = _mapper.Map<Villa>(villaCreateDto);

            await _db.Villas.AddAsync(modelo);//Al grabarse se asigna un id que es el que trabajamos nosotros
            await _db.SaveChangesAsync();
            //return CreatedAtRoute("GetVilla", new { id = villaCreateDto.Id }, villaCreateDto);//201
            return CreatedAtRoute("GetVilla", new { id = modelo.Id }, modelo);//201
        }

        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            //Agregar validación de existencia de la villa
            if (id == 0)
                return BadRequest("El id de la villa no puede ser 0");//400

            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);

            if (villa == null)
                return NotFound($"La villa con id {id} no existe");//404

            //VillaStore.VillaList.Remove(villa);
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();

            return NoContent();//204
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto)
        {
            if(villaUpdateDto==null || id!=villaUpdateDto.Id)
            {
                return BadRequest();//400
            }
            /*var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
            villa.Nombre = villaDto.Nombre;
            villa.Ocupantes = villaDto.Ocupantes;
            villa.MetrosCuadrados = villaDto.MetrosCuadrados;
            */
            
            /*Villa modelo = new()
            {
                Id = villaUpdateDto.Id,
                Nombre = villaUpdateDto.Nombre,
                Detalle = villaUpdateDto.Detalle,
                Tarifa = villaUpdateDto.Tarifa,
                Ocupantes = villaUpdateDto.Ocupantes,
                MetrosCuadrados = villaUpdateDto.MetrosCuadrados,
                ImagenUrl = villaUpdateDto.ImagenUrl,
                Amenidad = villaUpdateDto.Amenidad
            };
            */
            Villa modelo = _mapper.Map<Villa>(villaUpdateDto);

            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();

            return NoContent();//204
        }
    }
}
