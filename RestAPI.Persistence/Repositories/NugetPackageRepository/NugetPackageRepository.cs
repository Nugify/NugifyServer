using Microsoft.EntityFrameworkCore;
using RestAPI.Persistence.Models;

namespace RestAPI.Persistence.Repositories.NugetPackageRepository;

public class NugetPackageRepository : INugetPackageRepository
{
    private readonly NugifyContext _context;

    public NugetPackageRepository(NugifyContext context)
    {
        _context = context;
    }

    public async Task<Guid> Insert(string nugetPackageId, string version, string description, string authors)
    {
        var id = Guid.NewGuid();
        var entity = new NugetPackage
        {
            Id = id,
            Authors = authors,
            Description = description,
            Version = version,
            NugetPackageId = nugetPackageId
        };

        await _context.NugetPackages.AddAsync(entity);
        await _context.SaveChangesAsync();

        return id;
    }

    public async Task Delete(Guid nugetPackageId)
    {
        var entity = await _context.NugetPackages.FindAsync(nugetPackageId);
        
        if(entity == null)
            return;

        _context.NugetPackages.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<NugetPackage>> GetAll()
    {
        return await _context.NugetPackages.ToListAsync();
    }

    public async Task<NugetPackage?> GetById(Guid nugetPackageId)
    {
        return await _context.NugetPackages.FindAsync(nugetPackageId);
    }
}