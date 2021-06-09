using System.Collections.Generic;

namespace Fruits.Application.ViewModels
{
    public class ErrorViewModel
    {
        public List<string> Error { get; set; }

        public ErrorViewModel(List<string> error)
        {
            Error = error;
        }
    }
}
