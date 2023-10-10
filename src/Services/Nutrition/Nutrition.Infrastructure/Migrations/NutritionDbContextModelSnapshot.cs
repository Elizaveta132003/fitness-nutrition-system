﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nutrition.Infrastructure.Data.DataContext;

#nullable disable

namespace Nutrition.Infrastructure.Migrations
{
    [DbContext(typeof(NutritionDbContext))]
    partial class NutritionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Nutrition.Domain.Entities.Food", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Calories")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("Nutrition.Domain.Entities.FoodDiary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("FoodDiaries");
                });

            modelBuilder.Entity("Nutrition.Domain.Entities.MealDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FoodDiaryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MealType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FoodDiaryId");

                    b.ToTable("MealDetails");
                });

            modelBuilder.Entity("Nutrition.Domain.Entities.MealDish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FoodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MealDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("ServingSize")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("MealDetailId");

                    b.ToTable("MealDishes");
                });

            modelBuilder.Entity("Nutrition.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Nutrition.Domain.Entities.FoodDiary", b =>
                {
                    b.HasOne("Nutrition.Domain.Entities.User", "User")
                        .WithOne("FoodDiary")
                        .HasForeignKey("Nutrition.Domain.Entities.FoodDiary", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Nutrition.Domain.Entities.MealDetail", b =>
                {
                    b.HasOne("Nutrition.Domain.Entities.FoodDiary", "FoodDiary")
                        .WithMany("MealDetails")
                        .HasForeignKey("FoodDiaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodDiary");
                });

            modelBuilder.Entity("Nutrition.Domain.Entities.MealDish", b =>
                {
                    b.HasOne("Nutrition.Domain.Entities.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nutrition.Domain.Entities.MealDetail", "MealDetail")
                        .WithMany("MealDishes")
                        .HasForeignKey("MealDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("MealDetail");
                });

            modelBuilder.Entity("Nutrition.Domain.Entities.FoodDiary", b =>
                {
                    b.Navigation("MealDetails");
                });

            modelBuilder.Entity("Nutrition.Domain.Entities.MealDetail", b =>
                {
                    b.Navigation("MealDishes");
                });

            modelBuilder.Entity("Nutrition.Domain.Entities.User", b =>
                {
                    b.Navigation("FoodDiary")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
