using Mapster;

namespace QuizItEasy.Application.Features.SingleSelect.GetById;

public class SingleSelectResponseMapperConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Entities.Questions.SingleSelectQuestion, SingleSelectResponse>()
            .Map(dest => dest.Text, src => src.Text);
    }
}
