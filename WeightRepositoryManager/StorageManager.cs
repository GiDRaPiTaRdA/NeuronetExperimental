using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using StaticResourse;
using WeightManagment.WeightModel;

namespace WeightRepositoryManager
{
    public static class StorageManager
    {
        public static string SaveWeight(Weight weight, string name,string path = null)
        { 
            path =  path==null||!Directory.Exists(path)? 
                Path.Combine(StaticResourses.DefaultDirectoryForWeightRepository, name):
                Path.Combine(path, name);

            path = Path.ChangeExtension(path, StaticResourses.WeightExtention);

            BinaryFormatter binaryFormatter =  new BinaryFormatter();

            using (FileStream streamWriter = File.OpenWrite(path))
            {
                binaryFormatter.Serialize(streamWriter, weight);
            }

            return path;
        }

        public static Weight LoadWeight(string path)
        {
            Weight weight;

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream streamWriter = File.OpenRead(path))
            {
                weight = (Weight)binaryFormatter.Deserialize(streamWriter);
            }

            return weight;
        }
    }
}
