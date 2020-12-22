using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Dominio;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;

namespace Persistencia
{
    public class CursoOnlineContext : IdentityDbContext<Usuario>
    {
        public CursoOnlineContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CursoInstructor>().HasKey(ci => new { ci.InstructorId, ci.CursoId });
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override DbQuery<TQuery> Query<TQuery>()
        {
            return base.Query<TQuery>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            return base.DisposeAsync();
        }

        public override EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        {
            return base.Entry(entity);
        }

        public override EntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            return base.Add(entity);
        }

        public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        {
            return base.AddAsync(entity, cancellationToken);
        }

        public override EntityEntry<TEntity> Attach<TEntity>(TEntity entity)
        {
            return base.Attach(entity);
        }

        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            return base.Update(entity);
        }

        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            return base.Remove(entity);
        }

        public override EntityEntry Add(object entity)
        {
            return base.Add(entity);
        }

        public override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default)
        {
            return base.AddAsync(entity, cancellationToken);
        }

        public override EntityEntry Attach(object entity)
        {
            return base.Attach(entity);
        }

        public override EntityEntry Update(object entity)
        {
            return base.Update(entity);
        }

        public override EntityEntry Remove(object entity)
        {
            return base.Remove(entity);
        }

        public override void AddRange(params object[] entities)
        {
            base.AddRange(entities);
        }

        public override Task AddRangeAsync(params object[] entities)
        {
            return base.AddRangeAsync(entities);
        }

        public override void AttachRange(params object[] entities)
        {
            base.AttachRange(entities);
        }

        public override void UpdateRange(params object[] entities)
        {
            base.UpdateRange(entities);
        }

        public override void RemoveRange(params object[] entities)
        {
            base.RemoveRange(entities);
        }

        public override void AddRange(IEnumerable<object> entities)
        {
            base.AddRange(entities);
        }

        public override Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default)
        {
            return base.AddRangeAsync(entities, cancellationToken);
        }

        public override void AttachRange(IEnumerable<object> entities)
        {
            base.AttachRange(entities);
        }

        public override void UpdateRange(IEnumerable<object> entities)
        {
            base.UpdateRange(entities);
        }

        public override void RemoveRange(IEnumerable<object> entities)
        {
            base.RemoveRange(entities);
        }

        public override object Find(Type entityType, params object[] keyValues)
        {
            return base.Find(entityType, keyValues);
        }

        public override ValueTask<object> FindAsync(Type entityType, params object[] keyValues)
        {
            return base.FindAsync(entityType, keyValues);
        }

        public override ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken)
        {
            return base.FindAsync(entityType, keyValues, cancellationToken);
        }

        public override TEntity Find<TEntity>(params object[] keyValues)
        {
            return base.Find<TEntity>(keyValues);
        }

        public override ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues)
        {
            return base.FindAsync<TEntity>(keyValues);
        }

        public override ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken)
        {
            return base.FindAsync<TEntity>(keyValues, cancellationToken);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<CursoInstructor> CursoInstructor { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Precio> Precio { get; set; }

        public override DatabaseFacade Database => base.Database;

        public override ChangeTracker ChangeTracker => base.ChangeTracker;

        public override IModel Model => base.Model;

        public override DbContextId ContextId => base.ContextId;

        public override DbSet<Usuario> Users { get => base.Users; set => base.Users = value; }
        public override DbSet<IdentityUserClaim<string>> UserClaims { get => base.UserClaims; set => base.UserClaims = value; }
        public override DbSet<IdentityUserLogin<string>> UserLogins { get => base.UserLogins; set => base.UserLogins = value; }
        public override DbSet<IdentityUserToken<string>> UserTokens { get => base.UserTokens; set => base.UserTokens = value; }
        public override DbSet<IdentityUserRole<string>> UserRoles { get => base.UserRoles; set => base.UserRoles = value; }
        public override DbSet<IdentityRole> Roles { get => base.Roles; set => base.Roles = value; }
        public override DbSet<IdentityRoleClaim<string>> RoleClaims { get => base.RoleClaims; set => base.RoleClaims = value; }
    }
}