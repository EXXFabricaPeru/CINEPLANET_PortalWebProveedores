using System;
using System.Collections.Generic;
using System.Text;

namespace EFHBlazzer.Shared.Entities
{
    public class GenericEnt
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool boolAux { get; set; }
        public string LongDesc { get; set; }
        public string Unit { get; set; }
        public string Ruta { get; set; }
        public string Mat { get; set; }
        public string Med { get; set; }
        public Genericlote[] Lotes { get; set; }
    }

    public class Genericlote
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
