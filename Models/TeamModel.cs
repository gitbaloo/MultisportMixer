using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skovbyprojekt.Models
{
    public class TeamModel
    {

        public int Id { get; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public TeamModel(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public TeamModel(string name, string description) 
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
