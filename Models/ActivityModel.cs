using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace skovbyprojekt.Models
{
    public class ActivityModel
    {
        public int Id { get; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string? Description { get; set; }

        public string? Tools { get; set; }

        public string? Rules { get; set; }


        //constructor for Database
        public ActivityModel(int id, string name, string type, string? description, string? tools, string? rules)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Description = description;
            this.Tools = tools;
            this.Rules = rules;
        }

        //Constructor for actually creating a new activity
        public ActivityModel(string name, string type, string? description, string? tools, string? rules)
        {
            this.Name = name;
            this.Type = type;
            this.Description = description;
            this.Tools = tools;
            this.Rules = rules;
        }
    }
}
