
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DishFinder.Models;

public class StallsController : Controller
{
    private readonly DishFinderDbContext _context;

    public StallsController(DishFinderDbContext context)
    {
        _context = context;
    }

    // GET: STALLS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Stalls.ToListAsync());
    }

    // GET: STALLS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var stall = await _context.Stalls
            .Include(stall => stall.MenuItems)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (stall == null)
        {
            return NotFound();
        }

        return View(stall);
    }

    // GET: STALLS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: STALLS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,Location,MenuItems")] Stall stall)
    {
        if (ModelState.IsValid)
        {
            _context.Add(stall);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(stall);
    }

    // GET: STALLS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var stall = await _context.Stalls.FindAsync(id);
        if (stall == null)
        {
            return NotFound();
        }
        return View(stall);
    }

    // POST: STALLS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Description,Location,MenuItems")] Stall stall)
    {
        if (id != stall.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(stall);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StallExists(stall.Id))
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
        return View(stall);
    }

    // GET: STALLS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var stall = await _context.Stalls
            .FirstOrDefaultAsync(m => m.Id == id);
        if (stall == null)
        {
            return NotFound();
        }

        return View(stall);
    }

    // POST: STALLS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var stall = await _context.Stalls.FindAsync(id);
        if (stall != null)
        {
            _context.Stalls.Remove(stall);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StallExists(int? id)
    {
        return _context.Stalls.Any(e => e.Id == id);
    }
}
