namespace Api.WeChip.Models
{
    public class ProdutoTipo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<Produto> Produto { get; set; }
    }
}
