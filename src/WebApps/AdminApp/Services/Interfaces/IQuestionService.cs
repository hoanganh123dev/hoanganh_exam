using Examination.Shared.Questions;
using Examination.Shared.SeedWork;
using System.Threading.Tasks;

namespace AdminApp.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<PagedList<QuestionDto>> GetQuestionsPagingAsync(QuestionSearch taskListSearch);
        Task<QuestionDto> GetQuestionByIdAsync(string id);
        Task<bool> CreateAsync(CreateQuestionRequest request);
        Task<bool> UpdateAsync(UpdateQuestionRequest request);
        Task<bool> DeleteAsync(string id);
    }
}
