using FacturadorMVC.Components.Pages.Cliente;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace FacturadorMVC.Modelos
{
    public class Cliente
    {
        public int Cli_ID { get; set; }
        public string RazonSocial { get; set; }
        public string CUIT { get; set; }
        public string Direccion { get; set; }
        public bool Deshabilitado { get; set; }


        public string URL_GetAllClientes() => "https://localhost:7066/api/Cliente/GetAllClientes";
        public string URL_AddCliente() => "https://localhost:7066/api/Cliente/AddCliente";
        public string URL_ActualizarCliente(int IDActualizar) => $"https://localhost:7066/api/Cliente/{IDActualizar}";
        public string URL_EliminarCliente(int IDAEliminar) => $"https://localhost:7066/api/Cliente/{IDAEliminar}";
        public string URL_GetClienteByID(int IDABuscar) => $"https://localhost:7066/api/Cliente/GetClienteByID/{IDABuscar}";

        public async Task<List<Cliente>> GetAllClientes()
        {
            try
            {
                var url = URL_GetAllClientes();

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

                        var lsClientes = JsonSerializer.Deserialize<List<Cliente>>(content, options);

                        return lsClientes;
                    }

                    return new List<Cliente>();

                }

            }
            catch (Exception ex)
            {
                return new List<Cliente>();
            }
        }

    }
}
