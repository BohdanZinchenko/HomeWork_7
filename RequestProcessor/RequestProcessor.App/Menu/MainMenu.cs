
using System.Linq;
using System.Threading.Tasks;
using RequestProcessor.App.Logging;

using RequestProcessor.App.Services;

namespace RequestProcessor.App.Menu
{
    /// <summary>
    /// Main menu.
    /// </summary>
    internal class MainMenu : IMainMenu
    {
        private readonly IRequestPerformer _performer;
        private readonly IOptionsSource _options;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="options">Options source</param>
        /// <param name="performer">Request performer.</param>
        /// <param name="logger">Logger implementation.</param>
        public MainMenu(
            IRequestPerformer performer, 
            IOptionsSource options, 
            ILogger logger)
        {
            _performer = performer;
            _options = options;
            _logger = logger;
        }

        public async Task<int> StartAsync()
        {
            
            var options = await _options.GetOptionsAsync();
            _logger.Log("Take list of files");
            var arrayOfOptions = options.Select(x=>x!=null).ToArray();
            _logger.Log("Start work ");
            var tasks = options.Select(opt => _performer.PerformRequestAsync(opt, opt)).ToArray();
            await Task.WhenAll(tasks);
            _logger.Log("All tasks Done");

            return 0;
        }
    }
}
