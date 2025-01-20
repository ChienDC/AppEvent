using Microsoft.AspNetCore.Mvc;
using AppEvent.Models;

namespace AppEvent.Controllers;

[Route("events")]
public class EventsController : Controller
{
    private static List<Event> Events = new()
    {
        new Event { Id = 1, Name = "Event 1", Date = DateTime.Now.AddDays(1), Location = "Location 1", Description = "Description 1", ImageUrl = "image1.jpg", Status = "Chưa bắt đầu" },
        new Event { Id = 2, Name = "Event 2", Date = DateTime.Now, Location = "Location 2", Description = "Description 2", ImageUrl = "image2.jpg", Status = "Đang diễn ra" },
        new Event { Id = 3, Name = "Event 3", Date = DateTime.Now.AddDays(-1), Location = "Location 3", Description = "Description 3", ImageUrl = "image3.jpg", Status = "Đã kết thúc" },
    };

    // Hiển thị danh sách sự kiện
    [HttpGet]
    public IActionResult Index()
    {
        return View(Events);
    }

    // Hiển thị chi tiết sự kiện
    [HttpGet("{id}")]
    public IActionResult Details(int id)
    {
        var ev = Events.FirstOrDefault(e => e.Id == id);
        if (ev == null) return NotFound("Sự kiện không tồn tại.");
        return View(ev);
    }

    // Hiển thị form thêm mới sự kiện
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    // Xử lý thêm mới sự kiện
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Event newEvent)
    {
        if (!ModelState.IsValid) return View(newEvent);

        newEvent.Id = Events.Any() ? Events.Max(e => e.Id) + 1 : 1;
        Events.Add(newEvent);

        return RedirectToAction(nameof(Index));
    }

    // Hiển thị form chỉnh sửa sự kiện
    [HttpGet("edit/{id}")]
    public IActionResult Edit(int id)
    {
        var ev = Events.FirstOrDefault(e => e.Id == id);
        if (ev == null) return NotFound("Sự kiện không tồn tại.");
        return View(ev);
    }

    // Xử lý cập nhật sự kiện
    [HttpPost("edit/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Event updatedEvent)
    {
        if (!ModelState.IsValid) return View(updatedEvent);

        var ev = Events.FirstOrDefault(e => e.Id == id);
        if (ev == null) return NotFound("Sự kiện không tồn tại.");

        ev.Name = updatedEvent.Name;
        ev.Date = updatedEvent.Date;
        ev.Location = updatedEvent.Location;
        ev.Description = updatedEvent.Description;
        ev.ImageUrl = updatedEvent.ImageUrl;
        ev.Status = updatedEvent.Status;

        return RedirectToAction(nameof(Index));
    }

    // Xóa sự kiện
    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var ev = Events.FirstOrDefault(e => e.Id == id);
        if (ev == null) return NotFound("Sự kiện không tồn tại.");

        Events.Remove(ev);

        return RedirectToAction(nameof(Index));
    }
}
