using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AnikLakDbContext
{
    public class Appointment
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [JsonIgnore]
        public Client? Client { get; set; }
        [NotMapped]
        public string? ClientName
        {
            get
            {
                return Client?.Name;
            }
        }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Status { get; set; } = "";
        public string ServiceListString { get; set; } = "";
        public string DiscountListString { get; set; } = "";
        [NotMapped]
        public string ToolListString
        {
            get
            {
                return SerializeToolList();
            }
        }
        [JsonIgnore]
        public List<Tool>? ToolList { get; set; }
        [NotMapped]
        public string MaterialListString
        {
            get
            {
                return SerializeMaterialList();
            }
        }
        [JsonIgnore]
        public List<Material>? MaterialList { get; set; }
        public decimal BasePrice { get; set; }
        public decimal FinalPrice { get; set; }

        private string SerializeToolList()
        {
            StringBuilder sb = new StringBuilder();

            if (ToolList == null || ToolList.Count == 0) return "";

            foreach (var tool in ToolList)
            {
                sb.Append(tool.Id + "&");
                sb.Append(tool.Name + "&");
                sb.Append(tool.Count + "&");
                sb.Append(tool.FunctioningCount + ";");
            }

            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        private string SerializeMaterialList()
        {
            StringBuilder sb = new StringBuilder();

            if (MaterialList == null || MaterialList.Count == 0) return "";

            foreach (var material in MaterialList)
            {
                sb.Append(material.Id + "&");
                sb.Append(material.Name + "&");
                sb.Append(material.Count + ";");
            }

            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }
    }
}
