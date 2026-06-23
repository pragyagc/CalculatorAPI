namespace CalculatorAPI.Data
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);          // Fetch one record
        Task<IEnumerable<T>> GetAllAsync();    // Fetch all records
        Task AddAsync(T entity);               // Insert new record
        Task UpdateAsync(T entity);            // Update existing record
        Task DeleteAsync(int id);
    }
}
