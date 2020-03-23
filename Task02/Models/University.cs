using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Tut2.Models
{
    public class University
    {
        public University()
        {
            Students = new HashSet<Student>();
            CreationDate = DateTime.Now.ToString("yyyy-mm-dd");
        }

        [XmlAttribute]
        public string Author { get; set; }

        [JsonPropertyName("CreatedAt")]
        [XmlAttribute(AttributeName = "CreatedAt")]
        public string CreationDate { get; set; }

        public HashSet<Student> Students { get; set; }
    }
}
