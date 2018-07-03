using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emonthly_web.Models
{
    public class Report
    {
        public string Customer { get; set; }
        public string Id { get; set; }
        public string Mois { get; set; }
        public List<Rubrique> rubriques { get; set; }
        public string Type => typeof(Report).Name.ToLower();
    }
}
