using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NeuralCore.NeuronManagment.Entities;

namespace NeuralCore.NeuronManagment
{
    public static class NeuroNetLinks
    {
        public static byte[] TakeMemorySnapshot(this NeuroNet network)=>
            network.NeuroLayers.ObjectToByteArray();

        public static void LoadMemorySnapshot(this NeuroNet network,byte[] memory)=>
            network.NeuroLayers = memory.ByteArrayToObject<List<Neuron>[]>();

        private static byte[] ObjectToByteArray(this object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private static T ByteArrayToObject<T>(this byte[] arrBytes)
        {
            T obj;
            using (MemoryStream memStream = new MemoryStream())
            {
                BinaryFormatter binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                obj = (T) binForm.Deserialize(memStream);
            }

            return obj;
        }
    }
}