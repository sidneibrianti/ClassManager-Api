using ClasManager.Domain.Contexts.Bookings.Entities;
using ClassManager.Domain.Contexts.Classes.Entities;
using ClassManager.Domain.Contexts.Shared.Enums;
using ClassManager.Domain.Contexts.Subscriptions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassManager.Data.Contexts.Bookings.Mappings;

public class BookingMap : IEntityTypeConfiguration<Booking>
{
  public void Configure(EntityTypeBuilder<Booking> builder)
  {
    builder.ToTable("Bookings");

    builder.HasKey(x => x.Id);

    builder.HasOne(e => e.User)
      .WithMany(u => u.Bookings)
      .HasForeignKey("UserId")
      .HasPrincipalKey(u => u.Id);

    builder.HasOne(e => e.ClassDay)
      .WithMany(t => t.Bookings)
      .HasForeignKey("ClassDayId")
      .HasPrincipalKey(c => c.Id);
  }
}