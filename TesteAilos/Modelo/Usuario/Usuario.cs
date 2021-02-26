using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TesteAilos.Modelo
{

    public class Usuario
    {
        public string nome { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string administrador { get; set; }

        public string _id { get; set; }
    }
}
