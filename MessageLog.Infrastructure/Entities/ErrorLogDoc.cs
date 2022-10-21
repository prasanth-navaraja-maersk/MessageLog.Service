﻿using Finance.Common.Database.Relational.Interfaces.Entities;

namespace MessageLog.Infrastructure.Entities;

public class ErrorLogDoc : IEntity<long>, ISystemCreateDate, ISystemModifiedDate
{
    public long Id { get; set; }
    public long MessageId { get; set; }
    public string ExternalIdentifier { get; set; }
    public string ErrorCategory { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime? SystemCreateDate { get; set; }
    public DateTime? SystemModifiedDate { get; }
}