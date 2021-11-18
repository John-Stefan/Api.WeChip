namespace Api.WeChip.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public bool FinalizaCliente { get; set; }
        public bool ContabilizaVenda { get; set; }
        public List<Cliente> Cliente { get; set; }
    }
}
