using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LaSede.Models
{
    class CanchasHoras
    {
        [JsonProperty("id_cancha_hora")]
        public int id { get; set; }
        [JsonProperty("id_cancha")]
        public int idCancha { get; set; }
        [JsonProperty("id_horario")]
        public int idHorario { get; set; }
        [JsonProperty("estado")]
        public int estado { get; set; }

        [JsonProperty("hora_inicio")]
        public string horaInicio { get; set; }
        [JsonProperty("hora_fin")]
        public string horaFin { get; set; }

        [JsonProperty("nombre")]
        public string cancha { get; set; }
    }
}
