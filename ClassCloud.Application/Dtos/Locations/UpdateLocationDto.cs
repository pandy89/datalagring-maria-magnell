using System.ComponentModel.DataAnnotations;

namespace ClassCloud.Application.Dtos.Locations;

public record UpdateLocationDto(
        [Required]
        [MinLength(1)] 
        [MaxLength(255)] 
        string Name,

        string DateTime,
        byte[] RowVersion
);


