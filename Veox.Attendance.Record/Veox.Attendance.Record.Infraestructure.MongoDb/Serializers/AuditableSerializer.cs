using MongoDB.Bson.Serialization;
using Veox.Attendance.Record.Domain.Common;

namespace Veox.Attendance.Record.Infraestructure.MongoDb.Serializers
{
    public static class AuditableSerializer
    {
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