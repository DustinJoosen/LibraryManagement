using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace LibraryManagement.Helpers
{
    public static class SettingsHelper
    {
        public static SettingsObject GetSettings()
        {
            using (var streamReader = new StreamReader ("properties/settings.json", Encoding.UTF8))
            {
                string contents = streamReader.ReadToEnd();
                return JsonSerializer.Deserialize<SettingsObject>(contents) ?? new();
            }
        }

        public static void SetSettings(SettingsObject settings)
        {
            using (var streamWriter = new StreamWriter ("properties/settings.json"))
            {
                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions()
                {
                    WriteIndented = true
                });
                streamWriter.Write(json);
            }
        }
    }

    public class SettingsObject
    {
        [Display(Name = "Max books per customer")]
        public int MaxBooksPerCustomer { get; set; }
    }
}
