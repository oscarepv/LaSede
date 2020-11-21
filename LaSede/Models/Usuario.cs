using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaSede.Models
{
    class Usuario
    {
        [JsonProperty("id_usuario")]
        public string id_Usuario { get; set; }
        [JsonProperty("nombres")]
        public string nombres { get; set; }
        [JsonProperty("apellidos")]
        public string apellidos { get; set; }
        [JsonProperty("correo")]
        public string correo { get; set; }
        [JsonProperty("password")]
        public string password { get; set; }
        [JsonProperty("estado")]
        public string estado { get; set; }
        public string documento { get; set; }
        public string telefono { get; set; }
        public string url_foto { get; set; }
    }
}
