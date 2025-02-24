using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.GenericCrud;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly GameDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(GameDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    private readonly HashSet<string> _allowedIncludes = new()
    {
        "Engine",
        "Publisher",
        "GameGenres.Genre",
        "GamePlatforms.Platform",
        "GameDevelopers.Developer",
        "GameReviews.Review.Reviewever"
    };

    private IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, string[]? includes, out List<string> invalidIncludes)
    {
        invalidIncludes = new List<string>();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                string trimmedInclude = include.Trim();
            
                if (_allowedIncludes.Contains(trimmedInclude))
                {
                    query = query.Include(trimmedInclude);
                }
                else
                {
                    invalidIncludes.Add(trimmedInclude);
                }
            }
        }

        return query;
    }
    
    public async Task<IEnumerable<TEntity>> GetAllAsync(string[]? includes = null)
    {
        IQueryable<TEntity> query = _dbSet;
        
        List<string> invalidIncludes;
        query = ApplyIncludes(query, includes, out invalidIncludes);
        
        if (invalidIncludes.Any())
        {
            return new { InvalidIncludes = invalidIncludes } as IEnumerable<TEntity> ?? Array.Empty<TEntity>(); 
        }

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id, string[]? includes = null)
    {
        IQueryable<TEntity> query = _dbSet;
    
        List<string> invalidIncludes;
        query = ApplyIncludes(query, includes, out invalidIncludes);
        
        if (invalidIncludes.Any())
        {
            return null;
        }

        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }




    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}