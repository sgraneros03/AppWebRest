namespace Clases.API.Modelo.DTO
{
    public class ClaseDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }  
        public DateTime Fecha { get; set; }
        public decimal Pago { get; set; }
    }
}
