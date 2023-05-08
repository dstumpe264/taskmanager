using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TaskManager.Web.Models;
using TaskManager.Web.Services;

namespace TaskManager.Web.Pages
{
    public class IndexModel : PageModel {

        private readonly IToDoService _toDoService;

        public IndexModel(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        public List<ToDo> ToDos { get; set; }


        public async Task OnGetAsync()
        {
        
            try
            {
                ToDos = (await _toDoService.GetAsync()).ToList();
            }
            catch (HttpRequestException)
            {
                ToDos = new List<ToDo>();
            }

        }


        // public async Task OnPostAsync(int id, string status){
        
            
        //     var todo = await _toDoService.GetAsync(id);
        //     if(todo != null){

        //         todo.Status = status;
        //         await _toDoService.PutAsync(todo);
        //     }
        //     // return RedirectToPage();

        //     // 
        // }

        public async Task<IActionResult> OnPostAsync(ToDo todo)
        {
            if (!ModelState.IsValid){
                return Page();
            }

            var isSuccess = await _toDoService.PutAsync(todo);

            return isSuccess ? RedirectToPage("./Index") : RedirectToPage("./Error");
        }
    }
}
