using KASHOP.BLL.Settings;
using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repo;
        public CategoryService (IGenericRepository<Category> repo)
        {
            _repo=repo;
        }
        public async Task<bool> CreateAsync(CategoryRequest request, CancellationToken cancellationToken = default)
        {
            if (!AreTranslationsValid(request.Translations)) return false;
            var names = request.Translations.Select(t => t.Name.ToLower()).ToList();

            var existing = await _repo.GetOneAsync(
                filter: c => c.Translations.Any(t => names.Contains(t.Name.ToLower()))
                );
            if ( existing is not null ) return false;

            var category = request.Adapt<Category>();
            await _repo.CreateAsync(category, cancellationToken);
            return true;
        }

       

        public async Task<List<CategoryResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _repo.GetAsync(
                includes: [c=>c.Translations],
                cancellationToken:cancellationToken);   

            return categories.Adapt<List<CategoryResponse>>();
        }

      

        public async Task<CategoryResponse?> GetByIdAsync(int Id, CancellationToken cancellationToken = default)
        {
            var category = await _repo.GetOneAsync(
                filter: c => c.Id == Id,
                includes: [c => c.Translations],
                cancellationToken: cancellationToken);
            return category.Adapt<CategoryResponse?>();
        }

      

        public Task<bool> UpdateAsync(int Id, CategoryRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        async Task<bool> ICategoryService.DeleteAsync(int Id, CancellationToken cancellationToken = default)
        {
            var category = await _repo.GetOneAsync(
                filter: c => c.Id == Id,
                cancellationToken: cancellationToken);
            if (category is null) return false;
            await _repo.RemoveAsync(category, cancellationToken);
            return true;
        }
        private static bool AreTranslationsValid(List<CategoryTranslation> translations)
        {
            var languages = translations.Select(x => x.Language).ToList();
            if (languages.Distinct().Count() != languages.Count()) return false;
            return languages.Count == SupportedLanguages.Languages.Length && SupportedLanguages.Languages.All(lang => languages.Contains(lang));

        }
        

    }
}
