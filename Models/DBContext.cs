using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CustomersFinder.Models.Domain;
using CustomersFinder.Models.Poco;
using Microsoft.EntityFrameworkCore;

namespace CustomrsFinder.Models
{
    public class CustomerFinderDB : DbContext
    {
        public DbSet<CustomerInfo> customer_info { get; set; }
        public DbSet<ActualPhoneStatus> actual_phone_status { get; set; }
        public DbSet<CustomerPhoneNumbers> customer_phone_numbers { get; set; }
        public DbSet<Manager> manager { get; set; }
        public DbSet<CustomerType> customer_type { get; set; }
        public DbSet<PositionStatus> position_status { get; set; }
        public DbSet<PositionEvents> position_events { get; set; }
        public DbSet<PositionEventsWithUsersName> PositionEventsWithUsers { get; set; }

        public CustomerFinderDB(DbContextOptions<CustomerFinderDB> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
