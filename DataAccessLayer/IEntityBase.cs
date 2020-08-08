using System;

namespace DataAccessLayer
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }
}
