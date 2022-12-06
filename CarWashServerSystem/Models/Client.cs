using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashServerSystem.Models
{
        [Serializable]
        public class Client
        {
            public int Id { get; set; }
            public string CarNumber { get; set; }
            public string CarBrand { get; set; }
            public string CarModel { get; set; }
            public float Price { get; set; }
        }
    }


