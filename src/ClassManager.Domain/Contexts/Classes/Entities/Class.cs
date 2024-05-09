using System;
using ClassManager.Domain.Shared.Entities;
using ClassManager.Domain.Contexts.Tenants.Entities;

namespace ClassManager.Domain.Contexts.Classes.Entities
{
  public class Class : Entity
  {
    protected Class()
    {

    }
    public Class(string name, Guid tenantId, string businessHour)
    {
      Name = name;
      TenantId = tenantId;
      BusinessHour = businessHour;
      CreatedAt = DateTime.Now;
      UpdatedAt = DateTime.Now;
    }

    public string Name { get; private set; } = null!;
    public Guid TenantId { get; private set; }
    public string BusinessHour { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Tenant Tenant { get; }

    public void ChangeClass(string name, string businessHour)
    {
      Name = name;
      BusinessHour = businessHour;
    }
  }
}