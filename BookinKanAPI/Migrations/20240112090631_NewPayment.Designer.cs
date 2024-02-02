﻿// <auto-generated />
using System;
using BookinKanAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookinKanAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240112090631_NewPayment")]
    partial class NewPayment
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookinKanAPI.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<int>("BookingStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAtBooking")
                        .HasColumnType("datetime2");

                    b.Property<int>("ItineraryId")
                        .HasColumnType("int");

                    b.Property<int>("PassengerId")
                        .HasColumnType("int");

                    b.Property<string>("SeatsSerialized")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("ItineraryId");

                    b.HasIndex("PassengerId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BookinKanAPI.Models.Cars", b =>
                {
                    b.Property<int>("CarsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarsId"));

                    b.Property<string>("CarBrand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarRegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryCar")
                        .HasColumnType("int");

                    b.Property<int>("ClassCarsId")
                        .HasColumnType("int");

                    b.Property<string>("DetailCar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriceSeat")
                        .HasColumnType("int");

                    b.Property<int>("QuantitySeat")
                        .HasColumnType("int");

                    b.Property<int>("StatusCar")
                        .HasColumnType("int");

                    b.HasKey("CarsId");

                    b.HasIndex("ClassCarsId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("BookinKanAPI.Models.ClassCars", b =>
                {
                    b.Property<int>("ClassCarsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassCarsId"));

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("ClassCarsId");

                    b.ToTable("ClassCars");
                });

            modelBuilder.Entity("BookinKanAPI.Models.Driver", b =>
                {
                    b.Property<int>("DriverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DriverId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Charges")
                        .HasColumnType("float");

                    b.Property<string>("DriverName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDCardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusDriver")
                        .HasColumnType("int");

                    b.HasKey("DriverId");

                    b.ToTable("Drivers");

                    b.HasData(
                        new
                        {
                            DriverId = 101,
                            Address = "null",
                            Charges = 0.0,
                            DriverName = "null",
                            IDCardNumber = "null",
                            Phone = "null",
                            StatusDriver = 0
                        });
                });

            modelBuilder.Entity("BookinKanAPI.Models.ImageCars", b =>
                {
                    b.Property<int>("ImageCarsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageCarsId"));

                    b.Property<int>("CarsId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageCarsId");

                    b.HasIndex("CarsId");

                    b.ToTable("ImageCars");
                });

            modelBuilder.Entity("BookinKanAPI.Models.Itinerary", b =>
                {
                    b.Property<int>("ItineraryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItineraryId"));

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("CarsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("IssueTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RouteCarsId")
                        .HasColumnType("int");

                    b.HasKey("ItineraryId");

                    b.HasIndex("CarsId");

                    b.HasIndex("RouteCarsId");

                    b.ToTable("Itineraries");
                });

            modelBuilder.Entity("BookinKanAPI.Models.OrderRent", b =>
                {
                    b.Property<int>("OrderRentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderRentId"));

                    b.Property<int>("OrderSatus")
                        .HasColumnType("int");

                    b.Property<int>("PassengerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderRentId");

                    b.HasIndex("PassengerId");

                    b.ToTable("OrderRents");
                });

            modelBuilder.Entity("BookinKanAPI.Models.OrderRentItem", b =>
                {
                    b.Property<int>("OrderRentItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderRentItemId"));

                    b.Property<int>("CarsId")
                        .HasColumnType("int");

                    b.Property<int>("CarsPrice")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimePickup")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTimeReturn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DriverId")
                        .HasColumnType("int");

                    b.Property<int>("OrderRentId")
                        .HasColumnType("int");

                    b.Property<string>("PlacePickup")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceReturn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderRentItemId");

                    b.HasIndex("CarsId");

                    b.HasIndex("DriverId");

                    b.HasIndex("OrderRentId");

                    b.ToTable("OrderRentItems");
                });

            modelBuilder.Entity("BookinKanAPI.Models.Passenger", b =>
                {
                    b.Property<int>("PassengerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PassengerId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IDCardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassengerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("PassengerId");

                    b.HasIndex("RoleId");

                    b.ToTable("Passengers");

                    b.HasData(
                        new
                        {
                            PassengerId = 1,
                            Email = "Admin@g.com",
                            IDCardNumber = "123456789",
                            PassengerName = "Admin",
                            Password = "$2a$11$VG8zSUyzV618cYlghaKZUe1z.Dkn7I10wyTIBARSRLcd39ZWUwIR.",
                            Phone = "0912345678",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("BookinKanAPI.Models.PaymentBooking", b =>
                {
                    b.Property<int>("PaymentBookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentBookingId"));

                    b.Property<int?>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("ClientSecret")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentIntentId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentBookingId");

                    b.HasIndex("BookingId");

                    b.ToTable("PaymentBookings");
                });

            modelBuilder.Entity("BookinKanAPI.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "Passenger"
                        });
                });

            modelBuilder.Entity("BookinKanAPI.Models.RouteCars", b =>
                {
                    b.Property<int>("RouteCarsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RouteCarsId"));

                    b.Property<string>("DestinationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RouteCarsId");

                    b.ToTable("RouteCars");
                });

            modelBuilder.Entity("BookinKanAPI.Models.Booking", b =>
                {
                    b.HasOne("BookinKanAPI.Models.Itinerary", "Itinerary")
                        .WithMany()
                        .HasForeignKey("ItineraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookinKanAPI.Models.Passenger", "Passenger")
                        .WithMany()
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Itinerary");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("BookinKanAPI.Models.Cars", b =>
                {
                    b.HasOne("BookinKanAPI.Models.ClassCars", "ClassCars")
                        .WithMany()
                        .HasForeignKey("ClassCarsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassCars");
                });

            modelBuilder.Entity("BookinKanAPI.Models.ImageCars", b =>
                {
                    b.HasOne("BookinKanAPI.Models.Cars", "Cars")
                        .WithMany("ImageCars")
                        .HasForeignKey("CarsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cars");
                });

            modelBuilder.Entity("BookinKanAPI.Models.Itinerary", b =>
                {
                    b.HasOne("BookinKanAPI.Models.Cars", "Cars")
                        .WithMany()
                        .HasForeignKey("CarsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookinKanAPI.Models.RouteCars", "RouteCars")
                        .WithMany()
                        .HasForeignKey("RouteCarsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cars");

                    b.Navigation("RouteCars");
                });

            modelBuilder.Entity("BookinKanAPI.Models.OrderRent", b =>
                {
                    b.HasOne("BookinKanAPI.Models.Passenger", "Passenger")
                        .WithMany()
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("BookinKanAPI.Models.OrderRentItem", b =>
                {
                    b.HasOne("BookinKanAPI.Models.Cars", "Cars")
                        .WithMany()
                        .HasForeignKey("CarsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookinKanAPI.Models.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId");

                    b.HasOne("BookinKanAPI.Models.OrderRent", "OrderRent")
                        .WithMany("OrderRentItems")
                        .HasForeignKey("OrderRentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cars");

                    b.Navigation("Driver");

                    b.Navigation("OrderRent");
                });

            modelBuilder.Entity("BookinKanAPI.Models.Passenger", b =>
                {
                    b.HasOne("BookinKanAPI.Models.Role", "Roles")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("BookinKanAPI.Models.PaymentBooking", b =>
                {
                    b.HasOne("BookinKanAPI.Models.Booking", "Booking")
                        .WithMany()
                        .HasForeignKey("BookingId");

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("BookinKanAPI.Models.Cars", b =>
                {
                    b.Navigation("ImageCars");
                });

            modelBuilder.Entity("BookinKanAPI.Models.OrderRent", b =>
                {
                    b.Navigation("OrderRentItems");
                });
#pragma warning restore 612, 618
        }
    }
}
