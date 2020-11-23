using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LaSede.Models
{
    class Canchas
    {
        [JsonProperty("id_cancha")]
        public int id { get; set; }

        [JsonProperty("nombre")]
        public string nombre { get; set; }

        [JsonProperty("valor")]
        public double valor { get; set; }

        [JsonProperty("estado")]
        public int estado { get; set; }

        [JsonProperty("id_tipo_cancha")]
        public int tipoCancha { get; set; }
    }
}
