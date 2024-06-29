using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Lombard_app
{
    public class Client
    {

        public string Name;
        public int Age;
        public string PhoneNumber;
        public List<Item> Items;

        public Client()
        {
            // пустий конструктор для десеріалізації
        }

        public Client(string name, int age, string phoneNumber)
        {
            
            Name = name;
            Age = age;
            PhoneNumber = phoneNumber;
            Items = new List<Item>();
        }

        public void AddItem(Item item, string filePath)
        {
            List<Client> clients = StorageManager.LoadClientsFromFile(filePath);

            // Знайти клієнта з тими ж даними
            Client existingClient = clients.FirstOrDefault(c => c.Name == this.Name && c.Age == this.Age && c.PhoneNumber == this.PhoneNumber);

            if (existingClient != null)
            {
                
                existingClient.Items.Add(item);
            }
            else
            {
                // Якщо не знайшли такого клієнта то додаємо нового клієнта з товаром
                this.Items.Add(item);
                clients.Add(this);
            }

            
            StorageManager.SaveClientsToFile(clients, filePath);
        }

        public double RemoveItem(string itemName, string filePath)
        {
            List<Client> clients = StorageManager.LoadClientsFromFile(filePath);

            // Знаходимо клієнта
            Client currentClient = clients.FirstOrDefault(c => c.Name == this.Name && c.PhoneNumber == this.PhoneNumber);
            if (currentClient != null)
            {
                // Знаходимо товар за назвою в клієнта
                Item itemToRemove = currentClient.Items.Find(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
                if (itemToRemove != null)
                {
                    Console.WriteLine($"Товар знайдено: {itemToRemove.Name}");

                    // Видаляємо товар 
                    currentClient.Items.Remove(itemToRemove);

                    
                    StorageManager.SaveClientsToFile(clients, filePath);

                    // Повертаємо суму, яку повинен сплатити клієнт за повернення товару
                    return itemToRemove.LoanAmount;
                }
                else
                {
                    // Якщо товар не знайдено в клієнта
                    MessageBox.Show($"Товар {itemName} не знайдено у клієнта {currentClient.Name}.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Якщо клієнта не знайдено
                MessageBox.Show($"Поточного клієнта з ім'ям {this.Name} та номером телефону {this.PhoneNumber} не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return 0; 
        }

        public string CheckItemStatus(string itemName, string filePath)
        {
            List<Client> clients = StorageManager.LoadClientsFromFile(filePath);

            // Знаходимо клієнта
            Client currentClient = clients.FirstOrDefault(c => c.Name == this.Name && c.PhoneNumber == this.PhoneNumber);
            if (currentClient != null)
            {
                // Знаходимо товар 
                Item itemToCheck = currentClient.Items.Find(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
                if (itemToCheck != null)
                {
                    // отримуємо та повертаємо інформацію про стан товару
                    string status = $"Назва товару: {itemToCheck.Name}\n";
                    status += $"Оціночна вартість: {itemToCheck.Valuation}\n";
                    status += $"Сума, видана під заставу: {itemToCheck.LoanAmount}\n";
                    status += $"Дата прийому: {itemToCheck.DateReceived}\n";
                    status += $"Термін зберігання (в днях): {itemToCheck.StorageDuration}\n";
                    status += $"Залишилось часу на зберігання (в днях): {itemToCheck.RemainingDuration}\n";
                    
                    return status;
                }
                else
                {
                    // Якщо товар не знайдено
                    MessageBox.Show($"Товар {itemName} не знайдено у клієнта {currentClient.Name}.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Якщо клієнта не знайдено
                MessageBox.Show($"Поточного клієнта з ім'ям {this.Name} та номером телефону {this.PhoneNumber} не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "";
        }

        public void AddClientToFile(string filePath)
        {
            List<Client> clients = StorageManager.LoadClientsFromFile(filePath);

            // Перевіряємо, чи клієнт вже існує в списку
            bool clientExists = clients.Any(c => c.Name == this.Name && c.Age == this.Age && c.PhoneNumber == this.PhoneNumber);

            if (!clientExists)
            {
                
                clients.Add(this);
            }
            else
            {
                // Якщо клієнт існує, оновлюємо його список товарів
                Client existingClient = clients.First(c => c.Name == this.Name && c.Age == this.Age && c.PhoneNumber == this.PhoneNumber);
                foreach (var item in this.Items)
                {
                    existingClient.Items.Add(item);
                }
            }

            
            StorageManager.SaveClientsToFile(clients, filePath);
        }

    }
}
