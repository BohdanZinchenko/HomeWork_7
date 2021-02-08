using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    internal class OptionsSource:IOptionsSource
    {
       
        private readonly string _link;

        public OptionsSource(string link)
        {
            _link = link;
        }
        public async Task<List<RequestOptions>> GetOptionsAsync()
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            try
            {
                var jsonFIle =
                    JsonConvert.DeserializeObject<List<RequestOptions>>(await File.ReadAllTextAsync(_link), settings);
                return jsonFIle;
            }
            catch
            {
                throw new FileLoadException();
            }
            
            
            
            
           
           

        }
    }
}