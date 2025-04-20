using QuizItEasy.Domain.Entities.Common;

namespace QuizItEasy.Domain.Entities.Questions;

public class SingleSelect : Question
{
    private readonly List<Answer> _answers = [];

    public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();

    private SingleSelect(
        IEnumerable<Answer> answers,
        string text,
        FileMetadata? image)
        : base(text, image)
    {
        _answers.AddRange(answers);
    }

    public static SingleSelect Create(
        IEnumerable<Answer> answers,
        string text,
        FileMetadata? image = null)
    {
        return new SingleSelect(answers, text, image);
    }
}
