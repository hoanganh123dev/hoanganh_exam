﻿using AutoMapper;
using Examination.Application.Extensions;
using Examination.Domain.AggregateModels.QuestionAggregate;
using Examination.Shared.Questions;
using Examination.Shared.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Examination.Application.Commands.V1.Questions.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, ApiResult<QuestionDto>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateQuestionCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateQuestionCommandHandler(
                IQuestionRepository QuestionRepository,
                ILogger<CreateQuestionCommandHandler> logger,
                 IMapper mapper,
                 IHttpContextAccessor httpContextAccessor
            )
        {
            _questionRepository = QuestionRepository;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<ApiResult<QuestionDto>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var itemToAdd = await _questionRepository.GetQuestionsByIdAsync(request.Content);
            if (itemToAdd != null)
            {
                _logger.LogError($"Item name existed: {request.Content}");
                return new ApiErrorResult<QuestionDto>($"Item name existed: {request.Content}");

            }
            var questionId = ObjectId.GenerateNewId().ToString();
            var answers = _mapper.Map<List<AnswerDto>, List<Answer>>(request.Answers);
            itemToAdd = new Question(questionId,
                                    request.Content,
                                    request.QuestionType,
                                    request.Level,
                                    request.CategoryId,
                                    answers,
                                    request.Explain, _httpContextAccessor.GetUserId());
            await _questionRepository.InsertAsync(itemToAdd);
            var result = _mapper.Map<Question, QuestionDto>(itemToAdd);
            return new ApiSuccessResult<QuestionDto>(result);
        }
    }
}
