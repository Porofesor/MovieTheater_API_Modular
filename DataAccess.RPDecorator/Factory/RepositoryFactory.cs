using DataAccess.EFCore.Extends;
using DataAccess.RPDecorator.Repository;
using DataAccess.RPDecorator.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.RPDecorator.Factory
{
    public class RepositoryFactory<TEntity>
        where TEntity : class
    {
        private IRepository<TEntity>? _softDeletedRepository;
        private IRepository<TEntity>? _entityRepository;
        //public IB ClassB => _classB ??= new ClassB();
        public RepositoryFactory<TEntity>()
        {
            
        
        }
    }
}
