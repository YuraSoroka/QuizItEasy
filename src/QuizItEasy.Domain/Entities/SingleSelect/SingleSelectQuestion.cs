using MediatR;
using MongoDB.Bson;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;

namespace QuizItEasy.Domain.Entities.Questions;

public class SingleSelectQuestion : Question
{
    private readonly List<Answer> _answers = [];

    public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();

    private SingleSelectQuestion(
        IEnumerable<Answer> answers,
        string text,
        ObjectId quizCollectionId,
        FileMetadata? image)
        : base(text, quizCollectionId, image)
    {
        _answers.AddRange(answers);
    }

    public static Result<SingleSelectQuestion> Create(
        IEnumerable<Answer> answers,
        string text,
        ObjectId quizCollectionId,
        FileMetadata? image = null)
    {
        var correctAnswers = answers.Where(a => a.IsCorrect);

        if (!correctAnswers.Any())
        {
            return Result.Failure<SingleSelectQuestion>(Error.Problem("SingleSelectQuestionError", "Can not create question without correct answer"));
        }

        if(correctAnswers.Count() > 1)
        {
            return Result.Failure<SingleSelectQuestion>(Error.Problem("SingleSelectQuestionError", "Can not create question with more than one correct answer"));
        }

        return new SingleSelectQuestion(
            answers,
            text,
            quizCollectionId,
            image);
    }

    public bool IsCorrectAnswer(Guid answerId)
    {
        return _answers
            .Any(a => a.Id == answerId && a.IsCorrect);
    }
}
