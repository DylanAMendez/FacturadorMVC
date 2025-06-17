using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FacturadorMVC.Components.Pages.Factura_Detalle;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;
using System;
using System.Threading;
using System.Xml.Linq;

namespace FacturadorMVC.Modelos
{
    public class Factura_Detalle
    {
        public int Fact_ID { get; set; }
        public int FC_DTL_ID { get; set; }
        public DateOnly FechaAlta { get; set; }
        public string ART_ID { get; set; }
        public decimal Cant { get; set; }
        public decimal Precio { get; set; }
        public decimal Moto { get; set; }


        public string URL_GetAllFactura_Detalle() => "https://localhost:7097/api/Factura_Detalle/GetAllFacturaDetalle";
        public string URL_AddFactura_Detalle() => "https://localhost:7097/api/Factura_Detalle/AddFacturaDetalle";
        public string URL_ActualizarFactura_Detalle(int IDActualizar) => $"https://localhost:7097/api/Factura_Detalle/{IDActualizar}";
        public string URL_EliminarFactura_Detalle(int IDAEliminar) => $"https://localhost:7097/api/Factura_Detalle/{IDAEliminar}";
        public string URL_GetFactura_DetalleByID(int IDABuscar) => $"https://localhost:7097/api/Factura_Detalle/{IDABuscar}";


        public async Task<List<Factura_Detalle>> GetAllFacturaDetalle()
        {
            try
            {
                var url = URL_GetAllFactura_Detalle();

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

                        var lsFacturaDetalle = JsonSerializer.Deserialize<List<Factura_Detalle>>(content, options);

                        return lsFacturaDetalle;
                    }

                    return new List<Factura_Detalle>();
                }
            }
            catch (Exception ex)
            {
                return new List<Factura_Detalle>();
            }
        }

        public async Task<decimal> GetQuery1()
        {
            try
            {
                var lsFacturaDetalle = await GetAllFacturaDetalle();

                var hoy = DateTime.Today;

                var mesAnterior = hoy.AddMonths(-1);

                var lsFacturaMesAnterior = lsFacturaDetalle.Where(x => x.FechaAlta.Month == mesAnterior.Month && x.FechaAlta.Year == mesAnterior.Year).ToList();

                var montosFacturadosSuperados = lsFacturaMesAnterior.Where(x => x.Moto >= 10000).Sum(x => x.Moto);

                return montosFacturadosSuperados;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<List<Factura_Detalle>> GetQuery3()
        {
            try
            {
                var lsFacturaDetalle = await GetAllFacturaDetalle();

                if (lsFacturaDetalle.Count < 1) return new List<Factura_Detalle>();

                var lsFacturasConIDAGD123 = lsFacturaDetalle.Where(x => x.ART_ID.Equals("AGD_123")).ToList();

                return lsFacturasConIDAGD123;
            }
            catch (Exception ex)
            {
                return new List<Factura_Detalle>();
            }
        }

    }
}
