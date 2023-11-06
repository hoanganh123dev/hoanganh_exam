using Examination.Dtos.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Application.Queries.V1.Categories.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public GetCategoryByIdQuery(string id)
        {
            Id = id;
        }
        public string Id { set; get; }
    }
}
