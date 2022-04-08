using System.ComponentModel.DataAnnotations;

namespace ILIA.SimpleStore.API.Models;

public class CustomerCreateModel
{
    [Required(ErrorMessage = ValidationMessages.RequiredField)]
    public string Name { get; set; }

    [Required(ErrorMessage = ValidationMessages.InvalidFormat)]
    public string Email { get; set; }
}
