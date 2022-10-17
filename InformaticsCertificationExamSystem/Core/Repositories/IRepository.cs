using Microsoft.EntityFrameworkCore;
using InformaticsCertificationExamSystem.Models;
namespace InformaticsCertificationExamSystem.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Dbset { get; }
        InformaticsCertificationExamSystem_DBContext DbContext { get; set; }
        IEnumerable<T> GetAll();
        T? GetByID(int id);
        void Insert(T TEntity);
        void Update(T TEntity);
        void Delete(int ID);
        void Save();
    }
}
