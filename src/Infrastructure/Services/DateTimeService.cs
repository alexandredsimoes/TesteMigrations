using TesteMigrations.Application.Common.Interfaces;
using System;

namespace TesteMigrations.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
