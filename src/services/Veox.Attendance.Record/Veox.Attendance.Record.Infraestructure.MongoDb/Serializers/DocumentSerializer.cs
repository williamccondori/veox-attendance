using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Veox.Attendance.Record.Domain.Entities.Common;

namespace Veox.Attendance.Record.Infraestructure.MongoDb.Serializers
{
    public static class DocumentSerializer
    {
        public static void MapId<T>(this BsonClassMap<T> cm) where T : IDocument
        {
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
        }

        public static void MapAuditableFields<T>(this BsonClassMap<T> cm) where T : IAuditable
        {
            cm.MapMember(c => c.IsActive).SetElementName("isActive");
            cm.MapMember(c => c.CreatedBy).SetElementName("createdBy");
            cm.MapMember(c => c.CreatedDate).SetElementName("createdDate");
            cm.MapMember(c => c.UpdatedBy).SetElementName("updatedBy");
            cm.MapMember(c => c.UpdatedDate).SetElementName("updatedDate");
        }
    }
}