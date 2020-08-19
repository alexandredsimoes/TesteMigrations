using TesteMigrations.Application.Common.Interfaces;
using TesteMigrations.Domain.Common;
using TesteMigrations.Domain.Entities;
using TesteMigrations.Infrastructure.Identity;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace TesteMigrations.Infrastructure.Persistence
{
    public class ApplicationDbContext : BaseApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions, ICurrentUserService currentUserService, IDateTime dateTime) : base(options, operationalStoreOptions, currentUserService, dateTime)
        {
        }
    }
}
