using Localization.Resources;
using System.ComponentModel.DataAnnotations;

namespace Domain.Constants
{
    public enum AisVesselType
    {
        [Display(Name = nameof(SharedResource.AisVesselTypeOther), ResourceType = typeof(SharedResource))]
        Other
    }
}
