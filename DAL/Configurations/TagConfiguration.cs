using EduHome.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduHome.DAL.Configurations
{
    public sealed class TagConfiguration : IEntityTypeConfiguration<EventTag>
    {
        public void Configure(EntityTypeBuilder<EventTag> builder)
        {
            //builder.HasOne(x => x.Tags)
            //.WithMany()
            //.HasForeignKey(x => x.);
        }
    }
}
