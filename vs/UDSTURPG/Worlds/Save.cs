using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework;

namespace RPG.Worlds
{
    public struct SaveWorldData
    {
        public string name;
        public byte[,] world;
    }

    public static class WorldSave
    {
        /// <summary>
        /// Otwarta lista plików zapisu
        /// </summary>
        private static StorageContainer container;
        private static List<SaveWorldData> dataContainer = new List<SaveWorldData>();

        //!? Properties region
        #region PROPERTIES
        public static StorageContainer Container
        {
            get { return container; }
        }
        public static List<SaveWorldData> DataContainer
        {
            get { return dataContainer; }
            set { dataContainer = value; }
        }
        #endregion
        //!? END of properties region

        /// <summary>
        /// sync container
        /// </summary>
        /// <param name="device"></param>
        private static void sync(StorageDevice device)
        {
            // Open a storage container.
            IAsyncResult result =
                device.BeginOpenContainer("Storage", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();
        }
        /// <summary>
        /// Usuń pliki z kontenera
        /// </summary>
        private static void deSync()
        {
            container.Dispose();
        }

        /// <summary>
        /// Check if file exist
        /// </summary>
        /// <param name="filename">Cała nazwa pliku</param>
        private static bool check(string filename)
        {
            // Sprawdź czy plik istnieje
            if (container.FileExists(filename))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Stwórz nowy zapis/świat
        /// </summary>
        /// <param name="save">Nazwa zapisu</param>
        public static void Create(StorageDevice device, string save)
        {
            Stream stream;
            sync(device);

            string filename = save + ".sav";
            if (check(filename))
            {
                throw new Exception("Plik już istnieje");
            }
            else
            {
                stream = container.CreateFile(filename);
            }
            stream.Close();
            deSync();
        }
        public static Stream Open(StorageDevice device, string save)
        {
            sync(device);
            string filename = save + ".sav";
            if (check(filename))
            {
                return container.OpenFile(filename, FileMode.Open);
            }
            else
            {
                throw new Exception("Plik nie istnieje");
            }
        }

        public static void Load(StorageDevice device, string save, ref SaveWorldData data)
        {
            Stream stream;
            stream = Open(device, save);

            XmlSerializer serializer = new XmlSerializer(typeof(SaveWorldData));
            data = (SaveWorldData)serializer.Deserialize(stream);

            stream.Close();
            deSync();
        }

        public static void Save(StorageDevice device, string save, SaveWorldData data)
        {
            Stream stream;
            stream = Open(device, save);

            // Convert the object to XML data and put it in the stream.
            XmlSerializer serializer = new XmlSerializer(typeof(SaveWorldData));
            serializer.Serialize(stream, data);

            stream.Close();
            deSync();
        }
        public static void Replace(StorageDevice device, string save)
        {

        }
    }
}
