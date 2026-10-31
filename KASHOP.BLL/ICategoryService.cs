using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CategoryResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> CreateAsync (CategoryRequest request, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(int Id, CategoryRequest request, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int Id, CancellationToken cancellationToken = default);
    }
}
