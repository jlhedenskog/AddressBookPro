using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Address_Book_Pro.Data;
using Address_Book_Pro.Models;
using Address_Book_Pro.Utilities;
using Microsoft.AspNetCore.Http;

namespace Address_Book_Pro.Controllers
{
    public class AddressController : Controller
    {
        private readonly AddressBookContext _context;

        public AddressController(AddressBookContext context)
        {
            _context = context;
        }

        // GET: Address
        public async Task<IActionResult> Index(int id = 0)
        {
            var model = new IndexViewModel();

            model.Addresses = await _context.Addresses.ToListAsync();

            if(id != 0)
            {
                model.SelectedAddress = model.Addresses.FirstOrDefault(a => a.Id == id);
                if (model.SelectedAddress == null)
                {
                    model.SelectedAddress = new Address();
                }
            }
            else
            {
                model.SelectedAddress = new Address();
            }
           

            return View(model);


        }

        // GET: Address/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            if (address.Avatar != null)
            {
                ViewData["Avatar"] = AvatarHelper.GetImage(address.Avatar, address.FileName);
            }
            else
            {
                ViewData["Avatar"] = "../../Images/icons8-cat-profile-50.png";
            }

            return View(address);
        }

        // GET: Address/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Avatar,Address1,Address2,City,State,ZipCode,Phone,Birthday,Category")] Address address, IFormFile avatar)
        {
            if (ModelState.IsValid)
            {
                address.DateAdded = DateTime.Now;

                if (avatar != null)
                {
                    address.FileName = avatar.FileName;
                    address.Avatar = AvatarHelper.PutImage(avatar);
                }

                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }


        // GET: Address/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Avatar,Address1,Address2,City,State,ZipCode,Phone,DateAdded")] Address address, IFormFile avatar)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (avatar != null)
                    {
                        address.Avatar = AvatarHelper.PutImage(avatar);
                        address.FileName = avatar.FileName;
                    }

                    _context.Update(address);
                    if (avatar == null)
                    {
                        _context.Entry(address).Property(p => p.Avatar).IsModified = false;
                    }
                    await _context.SaveChangesAsync();
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Address/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}
