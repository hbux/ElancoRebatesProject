using ElancoUI.Models;

namespace ElancoUI.Helpers
{
    public interface IFormHelper
    {
        void FormatFields(FormModel form, Dictionary<string, string> fields);
    }
}
