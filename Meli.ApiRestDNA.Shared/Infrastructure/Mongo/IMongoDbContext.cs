using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Meli.ApiRestDNA.Shared.Infrastructure.Mongo
{
    public interface IMongoDbContext<TDocument> where TDocument : IDocument
    {
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        Task InsertOneAsync(TDocument document);
        Task ReplaceOneAsync(TDocument document);
    }
}
