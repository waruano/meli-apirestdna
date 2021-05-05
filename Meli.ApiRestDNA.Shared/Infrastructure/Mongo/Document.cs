using System;
using MongoDB.Bson;

namespace Meli.ApiRestDNA.Shared.Infrastructure.Mongo
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
