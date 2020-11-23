using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace LaSede.Models
{
    class Reservas
    {
        [JsonProperty("id_reserva")]
        public int id { get; set; }

        [JsonProperty("fecha")]
        public string fecha { get; set; }

        [JsonProperty("observacion")]
        public string observacion { get; set; }

        [JsonProperty("id_usuario")]
        public int idUsuario { get; set; }

        [JsonProperty("id_cancha_hora")]
        public int idCanchaHora { get; set; }

        [JsonProperty("estado")]
        public int estado { get; set; }
        [JsonProperty("hora_inicio")]
        public string hora { get; set; }
        [JsonProperty("hora_fin")]
        public string fin { get; set; }
        [JsonProperty("nombre")]
        public string cancha { get; set; }
    }
}
