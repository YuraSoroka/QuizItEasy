using Mapster;
using QuizItEasy.Domain.Entities.Questions;

namespace QuizItEasy.Application.Features.SingleSelect.GetById;

public class SingleSelectQuestionResponseMapperConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SingleSelectQuestion, SingleSelectQuestionResponse>()
            .Map(dest => dest.Text, src => src.Text);
    }
}
