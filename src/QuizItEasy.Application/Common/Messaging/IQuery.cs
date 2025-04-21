using MediatR;
using QuizItEasy.Domain.Common;

namespace QuizItEasy.Application.Common.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
