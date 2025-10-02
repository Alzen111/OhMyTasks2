using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TasksManagerWeb.Data;
using TasksManagerWeb.Models;

namespace TasksManagerWeb.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly TasksDbContext _context;

        public IndexModel(TasksDbContext context)
        {
            _context = context;
        }

        public List<TaskItem> Tasks { get; set; } = new();

        [BindProperty]
        [Required(ErrorMessage = "Task description is required")]
        [StringLength(500, ErrorMessage = "Task description cannot exceed 500 characters")]
        public string NewTaskDescription { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            await LoadTasksAsync();
        }

        public async Task<IActionResult> OnPostAddTaskAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadTasksAsync();
                return Page();
            }

            try
            {
                var task = new TaskItem
                {
                    Description = NewTaskDescription,
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                };

                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Task added successfully!";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error adding task: " + ex.Message;
                await LoadTasksAsync();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostToggleCompleteAsync(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task != null)
                {
                    task.IsCompleted = !task.IsCompleted;
                    task.CompletedAt = task.IsCompleted ? DateTime.Now : null;
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = task.IsCompleted 
                        ? "Task marked as complete!" 
                        : "Task marked as incomplete!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Task not found.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error updating task: " + ex.Message;
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteTaskAsync(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task != null)
                {
                    _context.Tasks.Remove(task);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Task deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Task not found.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting task: " + ex.Message;
            }

            return RedirectToPage();
        }

        private async Task LoadTasksAsync()
        {
            Tasks = await _context.Tasks
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
    }
}