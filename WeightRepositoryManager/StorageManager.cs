using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaticResourse;
using WeightManagment.WeightModel;
using Serializers.XmlSerializator.Serializer;

namespace WeightRepositoryManager
{
    public class StorageManager
    {
        public string Path { get; set; }

        Lazy<SerializerXml<List<Weight>>> Serializer { get; set; }
        SerializerXml<List<Weight>> Value => Serializer.Value;

        public StorageManager()
        {
            Innitialize(StaticResourses.defaultDirectoryForWeightRepository);
        }
        public StorageManager(string path)
        {
            Innitialize(path);
        }
        
        private void Innitialize(string path)
        {
            this.Path = path;
            Serializer = new Lazy<SerializerXml<List<Weight>>>(() => new SerializerXml<List<Weight>>(Path));
        }

        public void Add(Weight weight)
        {
            var read = Value.Deserialize();
            read.Add(weight);
            Value.Serialize(read);
        }
        public List<Weight> Read(Weight weight)
        {
            SerializerXml<List<Weight>> serializer = new SerializerXml<List<Weight>>(Path);
            return serializer.Deserialize();
        }

        public void Clear()
        {
            SerializerXml<List<Weight>> serializer = new SerializerXml<List<Weight>>(Path);
            Value.Serialize(new List<Weight>());
        }
    }
}
