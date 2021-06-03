using System.Collections.Generic;

namespace Fruits.Application.ViewModels
{
    public class ErrosViewModel
    {
        public ErrosViewModel(List<string> erros)
        {
            Erros = erros;
        }

        public List<string> Erros { get; set; }
    }
}
