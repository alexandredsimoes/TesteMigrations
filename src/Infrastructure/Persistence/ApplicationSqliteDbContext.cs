using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteMigrations.Application.Common.Interfaces;

namespace TesteMigrations.Infrastructure.Persistence
{
    public class ApplicationSqliteDbContext : BaseApplicationDbContext
    {
        public ApplicationSqliteDbContext(DbContextOptions options,
                                          IOptions<OperationalStoreOptions> operationalStoreOptions,
                                          ICurrentUserService currentUserService,
                                          IDateTime dateTime) : base(options, operationalStoreOptions, currentUserService, dateTime)
        {
        }
    }
}
