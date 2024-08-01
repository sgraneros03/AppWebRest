using Microsoft.AspNetCore.Mvc;
using Modelo;


namespace Clases.MVC.Controllers
{
    public class ClasesController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClasesController(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7008/api/Clase");
        }

        // GET: Clases
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("");
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, $"Error al obtener las clases: {errorMessage}");
            }

            var clases = await response.Content.ReadFromJsonAsync<List<ClaseDTO>>();
            return View(clases);
        }
        // GET: clases/Details/5 
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Clase no encontrada.");
            }
            var clase = await response.Content.ReadFromJsonAsync<ClaseDTO>();
            return View(clase);
        }

        // GET: Clase/Create
        public IActionResult Create()
        {
            var clase = new ClaseDTO()
            {
                Fecha = DateTime.Today
            };
            return View(clase);
        }


        // POST: clase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClaseDTO clase)
        {
            if (ModelState.IsValid)
            {
                if (clase.Fecha == DateTime.MinValue)
                {
                    clase.Fecha = DateTime.Today;
                }
                var response = await _httpClient.PostAsJsonAsync("", clase);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Error al crear la clase.");
                }
                return RedirectToAction(nameof(Index));
            }

            return View(clase);
        }


        // GET: clase/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Clase no encontrada.");
            }
            var clase = await response.Content.ReadFromJsonAsync<ClaseDTO>();
            return View(clase);
        }

        // POST: Clases/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClaseDTO clase)
        {
            if (id != clase.Id)
            {
                return BadRequest("ID de clase no coincide.");
            }

            if (ModelState.IsValid)
            {
                if (clase.Fecha == DateTime.MinValue)
                {
                    clase.Fecha = DateTime.Today;
                }
                var response = await _httpClient.PutAsJsonAsync($"{id}", clase);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Error al actualizar la clase.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clase);
        }

        // POST: clases/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Error al eliminar la clase.");
            }
            return RedirectToAction(nameof(Index));
        }

    }
}