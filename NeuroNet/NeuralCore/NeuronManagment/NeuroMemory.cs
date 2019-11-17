using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NeuralCore.Entities;

namespace NeuralCore.NeuronManagment
{
    public static class NeuroMemory
    {
        public static void SaveSnapshot(Memory memory, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(path ?? throw new ArgumentNullException(nameof(path)), FileMode.Create))
            {
                formatter.Serialize(fs, memory);
            }
        }

        public static Memory LoadSnapshot(string path)
        {
            Memory memory;

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream streamWriter = File.OpenRead(path))
            {
                memory = (Memory)binaryFormatter.Deserialize(streamWriter);
            }

            return memory;
        }

        public static Memory TakeMemorySnapshot(this NeuroNet network) =>
            network.NeuroMemory;

        public static void LoadMemorySnapshot(this NeuroNet network, Memory memory) =>
            network.NeuroMemory = memory;
    }
}