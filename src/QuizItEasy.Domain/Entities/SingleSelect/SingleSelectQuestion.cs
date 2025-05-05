using MediatR;
using MongoDB.Bson;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.QuizCollections;

namespace QuizItEasy.Domain.Entities.Questions;

public class SingleSelectQuestion : Question
{
#pragma warning disable IDE0044 // Add readonly modifier
    private List<Answer> _answers = [];
#pragma warning restore IDE0044 // Add readonly modifier

    public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();

    // For mongoDb creator map
    private SingleSelectQuestion(
        IEnumerable<Answer> answers,
        string text,
        ObjectId quizCollectionId,
        FileMetadata? image)
        : base(text, quizCollectionId, image)
    {
        _answers.AddRange(answers);
    }

    public static async Task<Result<SingleSelectQuestion>> Create(
        IMongoRepository<QuizCollection> quizColletionRepository,
        IEnumerable<Answer> answers,
        string text,
        string quizCollectionId,
        FileMetadata? image = null)
    {
        var collection = await quizColletionRepository.FindByIdAsync(quizCollectionId);
        if (collection is null)
        {
            return Result.Failure<SingleSelectQuestion>(Error.NotFound("QuizCollectionNotFound", "Quiz collection not found"));
        }

        var correctAnswers = answers.Where(a => a.IsCorrect);

        if (!correctAnswers.Any())
        {
            return Result.Failure<SingleSelectQuestion>(Error.Problem("SingleSelectQuestionError", "Can not create question without correct answer"));
        }

        if (correctAnswers.Count() > 1)
        {
            return Result.Failure<SingleSelectQuestion>(Error.Problem("SingleSelectQuestionError", "Can not create question with more than one correct answer"));
        }

        return new SingleSelectQuestion(
            answers,
            text,
            ObjectId.Parse(quizCollectionId),
            image);
    }

    public bool IsCorrectAnswer(Guid answerId)
    {
        return _answers
            .Any(a => a.Id == answerId && a.IsCorrect);
    }
}
