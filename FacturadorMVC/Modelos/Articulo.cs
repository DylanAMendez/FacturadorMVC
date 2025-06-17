namespace FacturadorMVC.Modelos
{
    public class Articulo
    {
        public int Articulo_ID { get; set; }
        public string ART_ID { get; set; }
        public decimal Cant { get; set; }
        public decimal Precio { get; set; }
        public decimal Moto { get; set; }
        public int? Cli_ID { get; set; } = 0;
        public string? Nombre { get; set; } = "";


        public string URL_GetAllArticulos() => "https://localhost:7097/api/Articulo";
        public string URL_AddArticulo() => "https://localhost:7097/api/Articulo";
        public string URL_ActualizarArticulo(int IDActualizar) => $"https://localhost:7097/api/Articulo/{IDActualizar}";
        public string URL_EliminarArticulo(int IDAEliminar) => $"https://localhost:7097/api/Articulo/{IDAEliminar}";
        public string URL_GetArticuloByID(int IDABuscar) => $"https://localhost:7097/api/Articulo/{IDABuscar}";
    }
}
