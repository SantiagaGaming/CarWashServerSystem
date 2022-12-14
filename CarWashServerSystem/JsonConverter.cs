using CarWashServerSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashServerSystem
{
    public class JsonConverter
    {
        private List<Client> _objects = new List<Client>();
        private string _path = "Data/database.json";

        public void AddObject(Client obj)
        {
            UpdateRepo();
            Client temp = _objects.FirstOrDefault(i => i.Id == obj.Id);
            if(temp ==null)
                _objects.Add(obj);
            else
            {
                _objects.Remove(temp);
                _objects.Add(obj);
            }
            string jsonFile = _path;
            string jsonString = JsonConvert.SerializeObject(_objects.ToArray());
            File.WriteAllText(jsonFile, jsonString);
        }
        private void UpdateRepo()
        {
            var jsonFile = File.ReadAllText(_path);
            if (JsonConvert.DeserializeObject<Client[]>(jsonFile) != null)
                _objects = JsonConvert.DeserializeObject<Client[]>(jsonFile).ToList();
        }
        public void DeleteObject(int number)
        {
            UpdateRepo();
            Client obj = _objects.FirstOrDefault(c => c.Id == number);
            if (obj != null)
                _objects.Remove(obj);
            string jsonFile = _path;
            string jsonString = JsonConvert.SerializeObject(_objects.ToArray());
            File.WriteAllText(jsonFile, jsonString);
        }
        public string GetObject()
        {
            UpdateRepo();
            string jsonString = JsonConvert.SerializeObject(_objects.ToArray());
            return jsonString;
        }
    }
}
