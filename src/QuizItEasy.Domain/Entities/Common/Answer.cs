namespace QuizItEasy.Domain.Entities.Common;

public sealed class Answer : ValueObject
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public bool IsCorrect { get; private set; }
    public string Value { get; private set; }

    private Answer(bool isCorrect, string value)
    {
        IsCorrect = isCorrect;
        Value = value;
    }

    public static Answer CreateOption(string value, bool isCorrect)
    {
        return new Answer(value: value, isCorrect: isCorrect);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return IsCorrect;
    }
}
