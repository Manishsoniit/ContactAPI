using ContactManagementAPI.Models;
using Newtonsoft.Json;

namespace ContactManagementAPI.Services
{
    public class ContactService
    {
        private readonly string _filePath = "contacts.json";
        private List<Contact> _contacts;

        public ContactService()
        {
            if (File.Exists(_filePath))
            {
                var jsonData = File.ReadAllText(_filePath);
                _contacts = JsonConvert.DeserializeObject<List<Contact>>(jsonData) ?? new List<Contact>();
            }
            else
            {
                _contacts = new List<Contact>();
            }
        }

        public List<Contact> GetAll() => _contacts;

        public Contact? GetById(int id) => _contacts.FirstOrDefault(c => c.Id == id);

        public Contact Add(Contact contact)
        {
            contact.Id = _contacts.Any() ? _contacts.Max(c => c.Id) + 1 : 1;
            _contacts.Add(contact);
            SaveToFile();
            return contact;
        }

        public bool Update(Contact contact)
        {
            var existing = GetById(contact.Id);
            if (existing == null) return false;

            existing.FirstName = contact.FirstName;
            existing.LastName = contact.LastName;
            existing.Email = contact.Email;

            SaveToFile();
            return true;
        }

        public bool Delete(int id)
        {
            var contact = GetById(id);
            if (contact == null) return false;

            _contacts.Remove(contact);
            SaveToFile();
            return true;
        }

        private void SaveToFile()
        {
            var jsonData = JsonConvert.SerializeObject(_contacts, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }
    }
}
