using FluentValidation.Results;

namespace Fruits.Application.ViewModels
{
    public abstract class BaseViewModel
    {
        public int Id { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public abstract bool IsValid();
    }
}
