using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using CMAP.Models;

namespace CMAP.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CMAP.Model.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EventType")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<int>("PatientId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:DiscriminatorProperty", "EventType");

                    b.HasAnnotation("Relational:DiscriminatorValue", "Event");
                });

            modelBuilder.Entity("CMAP.Model.VitalSigns", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DiastolicPressure");

                    b.Property<int>("EventId");

                    b.Property<int>("Height");

                    b.Property<int>("PulseRate");

                    b.Property<int>("RespirationRate");

                    b.Property<int>("SystolicPressure");

                    b.Property<float>("Temperature");

                    b.Property<int>("Weight");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("CMAP.Model.VitalSignsEvent", b =>
                {
                    b.HasBaseType("CMAP.Model.Event");


                    b.HasAnnotation("Relational:DiscriminatorValue", "vitalSigns");
                });

            modelBuilder.Entity("CMAP.Model.VitalSigns", b =>
                {
                    b.HasOne("CMAP.Model.VitalSignsEvent")
                        .WithOne()
                        .HasForeignKey("CMAP.Model.VitalSigns", "EventId");
                });
        }
    }
}
