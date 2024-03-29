﻿using System.Text.Json.Serialization;

namespace Api.WeChip.Models
{
    public class Oferta
    {
        public int Id { get; set; }
        public decimal ValorTotal { get; set; }
        public int ClienteId { get; set; }

        [JsonIgnore]
        public Cliente Cliente { get; set; }
        public Endereco Endereco { get; set; }

        public List<OfertaProduto> OfertaProdutos { get; set; }
    }
}
