using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Lombard_app
{
    public static class StorageManager
    {
        public static List<Client> LoadClientsFromFile(string filePath)
        {
            List<Client> clients = new List<Client>();
            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Client>));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    try
                    {
                        clients = (List<Client>)serializer.Deserialize(reader);
                    }
                    catch (InvalidOperationException)
                    {
                        
                    }
                }
            }
            return clients;
        }

        public static void SaveClientsToFile(List<Client> clients, string filePath)
        {
            XmlSerializer serializerWrite = new XmlSerializer(typeof(List<Client>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializerWrite.Serialize(writer, clients);
            }
        }

        
        public static void UpdateStorageDurations(string filePath)
        {
            List<Client> clients = LoadClientsFromFile(filePath);

            // проходимо по кожному клієнту і оновлюємо термін зберігання для кожного товару
            for (int i = 0; i < clients.Count; i++)
            {
                for (int j = 0; j < clients[i].Items.Count; j++)
                {
                    var item = clients[i].Items[j];

                    item.UpdateRemainingDuration();
                    if (item.RemainingDuration == 0)
                    {
                        MessageBox.Show($"У товара {item.Name} для клієнта {clients[i].Name} закінчився термін зберігання.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                        clients[i].Items.RemoveAt(j);
                        j--; // зменшуємо індекспісля видалення
                    }
                }
            }

            
            SaveClientsToFile(clients, filePath);
        }
    }
}
