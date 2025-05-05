using MongoDB.Bson;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.Questions;
using QuizItEasy.Domain.Entities.QuizCollections;

namespace QuizItEasy.Domain.Entities.MultiSelect;

public class MultiSelectQuestion : Question
{
#pragma warning disable IDE0044 // Add readonly modifier
    private List<Answer> _answers = [];
#pragma warning restore IDE0044 // Add readonly modifier

    public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();

    private MultiSelectQuestion(
        IEnumerable<Answer> answers,
        string text,
        ObjectId quizCollectionId,
        FileMetadata? image)
        : base(text, quizCollectionId, image)
    {
        _answers.AddRange(answers);
    }

    public static async Task<Result<MultiSelectQuestion>> Create(
        IMongoRepository<QuizCollection> quizColletionRepository,
        IEnumerable<Answer> answers,
        string text,
        string quizCollectionId,
        FileMetadata? image = null)
    {
        var collection = await quizColletionRepository.FindByIdAsync(quizCollectionId);
        if (collection is null)
        {
            return Result.Failure<MultiSelectQuestion>(Error.NotFound("QuizCollectionNotFound", "Quiz collection not found"));
        }

        var correctAnswers = answers.Where(a => a.IsCorrect);

        if (correctAnswers.Count() < 2)
        {
            return Result.Failure<MultiSelectQuestion>(Error.Problem("MultiSelectQuestionError", "Can not create question with less than two correct answers"));
        }

        return new MultiSelectQuestion(
            answers,
            text,
            ObjectId.Parse(quizCollectionId),
            image);
    }

    public bool IsCorrectAnswer(IEnumerable<Guid> answerIds)
    {
        var correctAnswers = _answers
            .Where(a => a.IsCorrect)
            .Select(a => a.Id);

        return new HashSet<Guid>(answerIds)
            .SetEquals(correctAnswers);
    }
}
