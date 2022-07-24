using NfcPos.Application.Common.Interfaces;

namespace NfcPos.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
