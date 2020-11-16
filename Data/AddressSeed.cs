using Address_Book_Pro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book_Pro.Data
{
    public static class AddressSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AddressBookContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AddressBookContext>>()))
            {
                // Look for any movies.
                if (context.Addresses.Any())
                {
                    return;   // DB has been seeded
                }
                context.Addresses.AddRange(
                    new Address
                    {
                        FirstName = "Hungry",
                        LastName = "Hippo",
                        Birthday = new DateTimeOffset(1990, 5, 16, 1, 1, 1, new TimeSpan(1, 0, 0)),
                        Email = "veryhungry@gmail.com",
                        Address1 = "732 Apple Pie Court",
                        City = "Pieland",
                        State = "Ohio",
                        ZipCode = "83621",
                        Phone = "(431)236-5789",
                        Category = "Friend",
                        DateAdded = DateTimeOffset.Now
                    }, new Address
                    {
                        FirstName = "Smallish",
                        LastName = "Hippo",
                        Birthday = new DateTimeOffset(1993, 2, 12, 1, 1, 1, new TimeSpan(1, 0, 0)),
                        Email = "kindasmall@gmail.com",
                        Address1 = "732 Apple Pie Court",
                        City = "Pieland",
                        State = "Ohio",
                        ZipCode = "83621",
                        Phone = "(431)236-2105",
                        Category = "Friend",
                        DateAdded = DateTimeOffset.Now
                    }, new Address
                    {
                        FirstName = "Mama",
                        LastName = "Bear",
                        Birthday = new DateTimeOffset(1984, 10, 31, 1, 1, 1, new TimeSpan(1, 0, 0)),
                        Email = "bestbear@gmail.com",
                        Address1 = "627 Owl Heart Lane",
                        City = "Memphis",
                        State = "Texas",
                        ZipCode = "98765",
                        Phone = "(333)333-3333",
                        Category = "Colleague",
                        DateAdded = DateTimeOffset.Now.AddDays(-42)
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
