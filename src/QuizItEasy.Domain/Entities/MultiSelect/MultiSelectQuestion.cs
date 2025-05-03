using MongoDB.Bson;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.Questions;

namespace QuizItEasy.Domain.Entities.MultiSelect;

public class MultiSelectQuestion : Question
{
    private readonly List<Answer> _answers = [];

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

    public static Result<MultiSelectQuestion> Create(
        IEnumerable<Answer> answers,
        string text,
        ObjectId quizCollectionId,
        FileMetadata? image = null)
    {
        var correctAnswers = answers.Where(a => a.IsCorrect);

        if (correctAnswers.Count() < 2)
        {
            return Result.Failure<MultiSelectQuestion>(Error.Problem("MultiSelectQuestionError", "Can not create question with less than two correct answers"));
        }

        return new MultiSelectQuestion(
            answers,
            text,
            quizCollectionId,
            image);
    }

    public bool IsCorrectAnswer(IEnumerable<Guid> answerIds)
    {
        var correctAnswers = _answers
            .Where(a => a.IsCorrect)
            .Select(a => a.Id);

        return !correctAnswers.Except(answerIds).Any();
    }
}
