using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace ElancoUI.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            if (ShowRequestId)
            {
                _logger.LogError("Unexpected error. ReqID: {RequestId}, Trace: {Trace}",
                RequestId, HttpContext.TraceIdentifier);
            }
            else
            {
                _logger.LogError("Unexpected error. No requestID.", ShowRequestId);
            }
            
        }
    }
}
