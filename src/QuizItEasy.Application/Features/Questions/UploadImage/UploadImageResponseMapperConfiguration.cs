using Mapster;
using QuizItEasy.Domain.Entities.Common;

namespace QuizItEasy.Application.Features.Questions.UploadImage;

public class UploadImageResponseMapperConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FileMetadata, UploadImageResponse>();
    }
}
