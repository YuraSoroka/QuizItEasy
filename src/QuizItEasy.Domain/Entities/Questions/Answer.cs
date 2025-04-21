namespace QuizItEasy.Domain.Entities.Questions;

public sealed class Answer
{
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

}
