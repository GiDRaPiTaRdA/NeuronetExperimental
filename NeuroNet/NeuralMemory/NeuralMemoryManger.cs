using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NeuralCore.NeuronManagment;

namespace NeuralMemory
{
    public static class NeuralMemoryManger
    {
        public static string NeuralSanapshotExtention => ".nnms";


        public static void SaveSnapshot(byte[] memoryBytes, string path)
        {
            path = Path.ChangeExtension(path, NeuralSanapshotExtention);
        
            BinaryFormatter formatter = new BinaryFormatter();
     
            using (FileStream fs = new FileStream(path ?? throw new ArgumentNullException(nameof(path)), FileMode.Append))
            {
                formatter.Serialize(fs, memoryBytes);
            }
        }

        public static byte[] LoadSnapshot(string path)
        {
            byte[] memoryBytes;

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream streamWriter = File.OpenRead(path))
            {
                memoryBytes = (byte[])binaryFormatter.Deserialize(streamWriter);
            }

            return memoryBytes;
        }

    }
}