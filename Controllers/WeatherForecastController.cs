using Microsoft.AspNetCore.Mvc;

namespace PrimerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private static List<WeatherForecast> ListaWeatherForecast = new List<WeatherForecast>();
        
        public WeatherForecastController() // Uso del constructor de la clase WeatherForecastController
        {
            if (ListaWeatherForecast.Count == 0)
            {
                ListaWeatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }).ToList();
            }
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return ListaWeatherForecast;        
        }

        [HttpGet()]
        [Route("{id}")]
        public ActionResult<WeatherForecast> GetByPosition(int id)
        {
            if (id < 0 || id >= ListaWeatherForecast.Count)
            {
                return BadRequest($"Índice {id} no válido. La lista tiene {ListaWeatherForecast.Count} elementos.");
            }
            return Ok(ListaWeatherForecast[id]);
        }

        [HttpPost]
        //(opcion1 - devolvemos el status)este tipo de valor devuelto es con el actionresult 
        //public ActionResult Post([FromBody] WeatherForecast nuevoClima)
        //(opcion 2 cuando se devuelve la lista)
        public IEnumerable<WeatherForecast> Post([FromBody] WeatherForecast nuevoClima)
        {
            ListaWeatherForecast.Add(nuevoClima);

            //(opcion1 - cuando se devuelve el status) return con el actionresult -
            //return Ok($"Elemento creado con exito en el índice {ListaWeatherForecast.Count - 1}. La lista ahora tiene {ListaWeatherForecast.Count} elementos. "); // Retorna 200 OK
            //(opcion 2 cuando se devuelve la lista)
            return ListaWeatherForecast;
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult Put(int id, [FromBody] WeatherForecast climaActualizado)
        {
            if (id < 0 || id >= ListaWeatherForecast.Count)
            {
                return BadRequest("No se encuentra el indice, no es posible realizar la actualizacion");
            }
            ListaWeatherForecast[id] = climaActualizado;
            return Ok($"Índice {id} Actualizado con exito.");
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0 || id >= ListaWeatherForecast.Count)
            {
                return BadRequest("No se encuentra el indice, no es posible realizar la eliminacion");
            }
            ListaWeatherForecast.RemoveAt(id);
            return Ok("Registro Eliminado con exito");
        }

    }
}
