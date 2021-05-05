using MediatR;

namespace Meli.ApiRestDNA.Application.Events
{
    public class SavedHumanEvent:INotification
    {
        public bool IsMutant { get; set; }
    }
}
