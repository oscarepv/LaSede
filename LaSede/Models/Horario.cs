using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LaSede.Models
{
    class Horario
    {
        [JsonProperty("id_horario")]
        public int id { get; set; }
        [JsonProperty("estado")]
        public int estado { get; set; }
        [JsonProperty("hora_inicio")]
        // [JsonConverter(typeof(Herramientas.ConvertidorTimeSpan))]

        public DateTime horaInicio { get; set; }

        [JsonProperty("hora_fin")]
       // [JsonConverter(typeof(Herramientas.ConvertidorTimeSpan))]

        public DateTime horaFin { get; set; }

    }
}
