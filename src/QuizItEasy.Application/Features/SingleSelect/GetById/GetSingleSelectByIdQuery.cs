using MongoDB.Bson;
using QuizItEasy.Application.Common.Messaging;

namespace QuizItEasy.Application.Features.SingleSelect.GetById;

public record GetSingleSelectByIdQuery(ObjectId Id) : IQuery<SingleSelectResponse>;
