using MongoDB.Bson.Serialization.Attributes;
using System;
using MongoDB.Bson;

namespace Meli.ApiRestDNA.Shared.Infrastructure.Mongo
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTime CreatedAt { get; }
    }
}
