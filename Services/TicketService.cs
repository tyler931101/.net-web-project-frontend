using frontend.Models;

namespace frontend.Services
{
    public class TicketService
    {
        private readonly ApiService _api;

        public TicketService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<TicketModel>> GetTicketsAsync()
        {
            
            return await _api.GetAsync<List<TicketModel>>("api/ticket") ?? new();
        }

        public async Task<TicketModel?> GetTicketAsync(string id)
        {
            return await _api.GetAsync<TicketModel>($"api/ticket/{id}");
        }

        public async Task<TicketModel?> CreateTicketAsync(TicketModel ticket)
        {
            Console.Write(ticket);
            Console.WriteLine($"[DEBUG Frontend] Sending ticket: {ticket.Title}, Performer={ticket.PerformerId}, ExpireDate={ticket.ExpireDate}");
            // POST → returns created ticket
            return await _api.PostAsync<TicketModel, TicketModel>("api/ticket", ticket);
        }

        public async Task<bool> UpdateTicketAsync(string id, TicketModel ticket)
        {
            // PUT → backend returns NoContent, so just check success
            await _api.PutAsync($"api/ticket/{id}", ticket);
            return true;
        }

        public async Task<bool> DeleteTicketAsync(string id)
        {
            // DELETE → backend returns NoContent, so just check success
            await _api.DeleteAsync($"api/ticket/{id}");
            return true;
        }
    }
}