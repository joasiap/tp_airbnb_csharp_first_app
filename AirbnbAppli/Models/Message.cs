using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbAppli.Models
{
    public class Message
    {
        public int Id { get; set; }

        public virtual Utilisateur Destinateur { get; set; }

        public virtual Utilisateur Emetteur { get; set; }

        public DateTime Date { get; set; }

        public string Contenu { get; set; }
    }
}
