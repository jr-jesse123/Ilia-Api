using System.ComponentModel.DataAnnotations;

namespace ILIA.SimpleStore.API.Models;

public class CustomerCreateModel
{
    [Required(ErrorMessage = ValidationMessages.RequiredField, AllowEmptyStrings = false)]
    [MinLength(5, ErrorMessage = ValidationMessages.MinLength)]
    public string Name { get; set; }

    [EmailAddress(ErrorMessage = ValidationMessages.InvalidFormat)]
    public string Email { get; set; }
}
