using MongoDB.Bson;
using QuizItEasy.Domain.Entities.Common;

namespace QuizItEasy.Domain.Entities.Questions;

public class SingleSelect : Question
{
    private readonly List<Answer> _answers = [];

    public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();

    private SingleSelect(
        IEnumerable<Answer> answers,
        string text,
        ObjectId quizCollectionId,
        FileMetadata? image)
        : base(text, quizCollectionId, image)
    {
        _answers.AddRange(answers);
    }

    public static SingleSelect Create(
        IEnumerable<Answer> answers,
        string text,
        ObjectId quizCollectionId,
        FileMetadata? image = null)
    {
        return new SingleSelect(
            answers, 
            text, 
            quizCollectionId, 
            image);
    }
}
