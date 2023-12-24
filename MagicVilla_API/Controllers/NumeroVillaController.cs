using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;



namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        //private readonly ApplicationDbContext _db ;
        private readonly IVillaRepositorio _villaRepo;
        private readonly INumeroVillaRepositorio _numeroRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepo, INumeroVillaRepositorio numeroRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numeroRepo = numeroRepo;
            _mapper = mapper;
            _response = new ();
        }

        //De la interfaz IActionResult para cualquier tipo de retorno
        //Lo volvemos asincrono para que no se bloquee el hilo
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                //Ya no va a devolver IEnumerable<VillaDto> sino APIResponse
                _logger.LogInformation("Obtener Numero Villas");
                IEnumerable<NumeroVilla> numeroVillaList = await _numeroRepo.ObtenerTodos();
                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numeroVillaList);
                _response.statusCode = HttpStatusCode.OK;
                //IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
                //200
                //return Ok(VillaStore.VillaList);
                //Es como si hicieramos un selec * from villas
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("id:int", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                //Agregar validación de existencia de la villa
                if (id == 0)
                {
                    _logger.LogError("El id del numero villa no puede ser 0");
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                    //return BadRequest("El id de la villa no puede ser 0");//400
                }
                //var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
                var numeroVilla = await _numeroRepo.Obtener(v => v.VillaNo == id);

                if (numeroVilla == null) {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                    //return NotFound($"La villa con id {id} no existe");//404
                }
                _response.Resultado = _mapper.Map<NumeroVillaDto>(numeroVilla);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDto villaCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;  
                    return BadRequest(_response);
                    //return BadRequest(ModelState);//400         
                }

                if (await _numeroRepo.Obtener(v => v.VillaNo == villaCreateDto.VillaNo) != null)
                {
                    ModelState.AddModelError("Nombre", "El nombre de la villa ya existe");
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                    //return BadRequest(ModelState);//400
                }
                if(await _villaRepo.Obtener(v => v.Id == villaCreateDto.VillaId) == null)
                {
                    ModelState.AddModelError("Villa", "La villa no existe");
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                    //return BadRequest(ModelState);//400
                }
                //Agregar validación de existencia de la villa
                if (villaCreateDto == null)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                    //return BadRequest("La villa no puede ser nula");//400
                }
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
                NumeroVilla modelo = _mapper.Map<NumeroVilla>(villaCreateDto);

                modelo.FechaCreación = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;

                await _numeroRepo.Crear(modelo);//Al grabarse se asigna un id que es el que trabajamos nosotros
                                               //await _db.SaveChangesAsync();
                                               //return CreatedAtRoute("GetVilla", new { id = villaCreateDto.Id }, villaCreateDto);//201
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = modelo.VillaNo },_response);//201
            }
            catch(Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

            
        }

        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> DeleteNumeroVilla(int id)
        {
            try
            {
                //Agregar validación de existencia de la villa
                if (id == 0)
                {
                    // return BadRequest("El id de la villa no puede ser 0");//400
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso = false;
                    return BadRequest(_response);
                }
                var numeroVilla = await _numeroRepo.Obtener(v => v.VillaNo == id);

                if (numeroVilla == null)
                {
                    //return NotFound($"La villa con id {id} no existe");//404
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }

                //VillaStore.VillaList.Remove(villa);
                await _numeroRepo.Remover(numeroVilla);
                //await _db.SaveChangesAsync();
                _response.statusCode = HttpStatusCode.NoContent;
                
                return Ok(_response);//204
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            //Las interfaces no pueden llevar un tipo de dato
            //return _response;
            return BadRequest(_response);
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto villaUpdateDto)
        {
            if(villaUpdateDto==null || id!=villaUpdateDto.VillaNo)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                return BadRequest(_response);
                //return BadRequest();//400
            }
            if(await _villaRepo.Obtener(v => v.Id == villaUpdateDto.VillaId) == null)
            {
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                return BadRequest(_response);
                //return BadRequest("La villa no existe");//400
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
            NumeroVilla modelo = _mapper.Map<NumeroVilla>(villaUpdateDto);

            await _numeroRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;

            //await _db.SaveChangesAsync();

            //return NoContent();//204
            return Ok(_response);
        }
    }
}
