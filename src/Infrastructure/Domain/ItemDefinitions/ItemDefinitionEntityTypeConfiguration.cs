using Accumulator.Domain.ItemDefinitions;
using Accumulator.Domain.Items;
using Accumulator.Infrastructure.Modules.Data;
using Ardalis.SmartEnum.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accumulator.Infrastructure.Domain.ItemDefinitions;

internal sealed class ItemDefinitionEntityTypeConfiguration : EntityTypeConfiguration<ItemDefinition>
{
    public override void Configure(EntityTypeBuilder<ItemDefinition> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable(nameof(AccumulatorDbContext.ItemDefinitions));

        builder.HasKey(item => item.Code);

        builder.Property(item => item.Code)
            .HasColumnOrder(NextOrder);

        builder.Property(item => item.Type)
            .HasColumnOrder(NextOrder)
            .HasConversion<SmartEnumConverter<ItemType, int>>();

        builder.Property(item => item.Name)
            .HasColumnOrder(NextOrder);

        builder.Property(item => item.GradeType)
            .HasColumnOrder(NextOrder)
            .HasConversion<SmartEnumConverter<ItemGradeType, int>>();

        builder.Property(item => item.Version)
            .HasColumnOrder(NextOrder)
            .HasConversion<SmartEnumConverter<ItemVersion, byte>>();

        builder.Property(item => item.IsStackable)
            .HasColumnOrder(NextOrder);

        builder.OwnsOne(
            item => item.Size,
            sizeBuilder =>
            {
                sizeBuilder.Property(size => size.Height)
                    .HasColumnName(nameof(ItemSize.Height))
                    .HasColumnOrder(NextOrder);

                sizeBuilder.Property(size => size.Width)
                    .HasColumnName(nameof(ItemSize.Width))
                    .HasColumnOrder(NextOrder);
            });

        builder.OwnsMany(
            item => item.Grades,
            gradeBuilder =>
            {
                gradeBuilder.ToTable("ItemDefinitionGrades");

                gradeBuilder.WithOwner()
                    .HasForeignKey(grade => grade.ParentCode);

                gradeBuilder.HasKey(grade => new { grade.ParentCode, grade.Code, });

                gradeBuilder.Property(grade => grade.ParentCode)
                    .HasColumnOrder(NextSubOrder<ItemDefinitionGrade>());

                gradeBuilder.Property(grade => grade.Code)
                    .HasColumnOrder(NextSubOrder<ItemDefinitionGrade>());

                gradeBuilder.Property(item => item.Type)
                    .HasColumnOrder(NextSubOrder<ItemDefinitionGrade>())
                    .HasConversion<SmartEnumConverter<ItemGradeType, int>>();

                gradeBuilder.HasOne<ItemDefinition>()
                    .WithMany()
                    .HasForeignKey(grade => grade.Code);
            });
    }
}
