using MongoDB.Driver;
using subastasProjectFinal.Models;

namespace subastasProjectFinal.Services
{
    public class ReportService
    {
        private readonly IMongoCollection<Report> _reports;

        public ReportService(IMongoDatabase database)
        {
            _reports = database.GetCollection<Report>("Reports");
        }

        public async Task<List<Report>> GetAllAsync() =>
            await _reports.Find(_ => true).ToListAsync();

        public async Task<Report> GetByIdAsync(string id) =>
            await _reports.Find(report => report.Id == id).FirstOrDefaultAsync();

        public async Task<List<Report>> GetByReporterIdAsync(string reporterId) =>
            await _reports.Find(report => report.ReporterId == reporterId).ToListAsync();

        public async Task<List<Report>> GetByReportedUserIdAsync(string reportedUserId) =>
            await _reports.Find(report => report.ReportedUserId == reportedUserId).ToListAsync();

        public async Task CreateAsync(Report report)
        {
            report.Timestamp = DateTime.UtcNow;
            report.Status = "pending";
            await _reports.InsertOneAsync(report);
        }

        public async Task UpdateAsync(string id, Report report) =>
            await _reports.ReplaceOneAsync(r => r.Id == id, report);

        public async Task DeleteAsync(string id) =>
            await _reports.DeleteOneAsync(r => r.Id == id);
    }
}
