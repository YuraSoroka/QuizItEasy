using QuizItEasy.Domain.Entities.Common;

namespace QuizItEasy.Domain.Entities.QuizCollections;

public class QuizCollection : AggregateRoot
{
    public string Code { get; private set; }
    public string Name { get; private set; }

    private QuizCollection(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public static QuizCollection Create(string code, string name)
    {
        return new QuizCollection(code, name);
    }
}
