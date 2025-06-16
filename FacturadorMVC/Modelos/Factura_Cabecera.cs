using FacturadorMVC.Components.Pages.FacturaCabecera;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace FacturadorMVC.Modelos
{
    public class Factura_Cabecera
    {
        public int FC_ID { get; set; }
        public DateOnly FechaAlta { get; set; }
        public string Cli_ID { get; set; }
        public string Estado { get; set; }


        public string URL_GetAllFacturaCabecera() => "https://localhost:7066/api/Factura_Cabecera";
        public string URL_AddFacturaCabecera() => "https://localhost:7066/api/Factura_Cabecera";
        public string URL_ActualizarFacturaCabecera(int IDActualizar) => $"https://localhost:7066/api/Factura_Cabecera/{IDActualizar}";
        public string URL_EliminarFacturaCabecera(int IDAEliminar) => $"https://localhost:7066/api/Factura_Cabecera/{IDAEliminar}";
        public string URL_GetFacturaCabeceraByID(int IDABuscar) => $"https://localhost:7066/api/Factura_Cabecera/{IDABuscar}";


        public async Task<List<Factura_Cabecera>> GetAllFacturaCabecera()
        {
            try
            {
                var url = URL_GetAllFacturaCabecera();

                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var lsFacturaCabecera = JsonSerializer.Deserialize<List<Factura_Cabecera>>(content, options);

                        return lsFacturaCabecera;
                    }

                    return new List<Factura_Cabecera>();
                }
            }
            catch (Exception ex)
            {
                return new List<Factura_Cabecera>();
            }
        }


        public async Task<List<Factura_Cabecera>> GetQuery2()
        {
            try
            {
                Cliente oCliente = new Cliente();

                var lsAllClientes = await oCliente.GetAllClientes();

                if (lsAllClientes.Count < 1) return new List<Factura_Cabecera>();

                var lsAllClientesTerminanConCuit8 = lsAllClientes.Where(x => x.CUIT.EndsWith("8")).ToList();

                var lsAllFacturasCabecera = await GetAllFacturaCabecera();

                if(lsAllFacturasCabecera.Count < 1) return new List<Factura_Cabecera>();
          
                var IDClientesConCuit8 = lsAllClientesTerminanConCuit8.Select(c => c.Cli_ID).ToList();

                var lsAllFacturasContienenClientesCuit8 = lsAllFacturasCabecera.Where(x => IDClientesConCuit8.Contains(Convert.ToInt32(x.Cli_ID))).ToList();

                return lsAllFacturasContienenClientesCuit8;

            }
            catch (Exception ex)
            {
                return new List<Factura_Cabecera>();
            }
        }

    }
}
