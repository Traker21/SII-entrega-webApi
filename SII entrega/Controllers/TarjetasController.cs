using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SII_entrega.Entidades;

using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;

namespace SII_entrega.Controllers
{

    [ApiController]
    [Route("Api/Tarjetas")]
    public class TarjetasController : ControllerBase
    {

        string authSecret = "AuavHykMNBvXBXGW6yJ7znIti5t79QKA2HWNV36a";
        string basePath = "https://sii-gestion-tarjeta-default-rtdb.firebaseio.com/";

        IFirebaseClient cliente;



        public TarjetasController()
        {

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = authSecret,
                BasePath = basePath
            };

            cliente = new FirebaseClient(config);


        }







        [HttpPost]
        public IActionResult Post(Tarjetas tarjeta)
        {
            string IdGenerador = Guid.NewGuid().ToString("N");
            SetResponse response = cliente.Set("Tarjetas/" + IdGenerador, tarjeta);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok();
            }
            else
                return BadRequest();
        }







        [HttpGet]
        public IActionResult Get()
        {
            try

            {
                var listado  = new List<Tarjetas>();    

                FirebaseResponse response = cliente.Get("Tarjetas");


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    Dictionary<string, Tarjetas> data = response.ResultAs<Dictionary<string, Tarjetas>>();
                    if(data == null)
                    {
                        return Ok(listado);

                    }
                    foreach (var item in data)
                    {
                        listado.Add(item.Value);
                    }


                    return Ok(listado);
                }
                else
                {

                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }





        [HttpGet("{numero}")]
        public IActionResult Get(string numero)
        {
            try
            {

                FirebaseResponse response = cliente.Get("Tarjetas");


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    Dictionary<string, Tarjetas> data = response.ResultAs<Dictionary<string, Tarjetas>>();


                    var resultados = data.Where(x => x.Value.numero == numero).ToDictionary(x => x.Key, x => x.Value);


                    if (resultados.Any())
                    {

                        return Ok(resultados);
                    }
                    else
                    {

                        return NotFound("No se encontraron resultados para el nombre proporcionado");
                    }
                }
                else
                {

                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }







        [HttpPut("{numero}")]
        public IActionResult Put(string numero, [FromBody] Tarjetas tarjeta)
        {
            try
            {
                
                FirebaseResponse response = cliente.Get("Tarjetas");

                
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Dictionary<string, Tarjetas> data = response.ResultAs<Dictionary<string, Tarjetas>>();

                   
                    KeyValuePair<string, Tarjetas> tarjetaEncontrada = data.FirstOrDefault(x => x.Value.numero == numero);

                    if (tarjetaEncontrada.Key != null)
                    {
                        
                        SetResponse updateResponse = cliente.Set("Tarjetas/" + tarjetaEncontrada.Key, tarjeta);

                      
                        if (updateResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return Ok();
                        }
                        else
                        {
                           
                            return StatusCode((int)updateResponse.StatusCode, updateResponse);
                        }
                    }
                    else
                    {
                       
                        return NotFound("No se encontró la tarjeta con el número proporcionado");
                    }
                }
                else
                {
                    
                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{numero}")]
        public IActionResult Delete(string numero)
        {
            try
            {
                FirebaseResponse response = cliente.Get("Tarjetas");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Dictionary<string, Tarjetas> data = response.ResultAs<Dictionary<string, Tarjetas>>();

                    KeyValuePair<string, Tarjetas> tarjetaEncontrada = data.FirstOrDefault(x => x.Value.numero == numero);

                    if (tarjetaEncontrada.Key != null)
                    {
                        FirebaseResponse deleteResponse = cliente.Delete("Tarjetas/" + tarjetaEncontrada.Key);

                        if (deleteResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return Ok();
                        }
                        else
                        {
                            return StatusCode((int)deleteResponse.StatusCode);
                        }
                    }
                    else
                    {
                        return NotFound("No se encontró la tarjeta con el número proporcionado");
                    }
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




    }

}
