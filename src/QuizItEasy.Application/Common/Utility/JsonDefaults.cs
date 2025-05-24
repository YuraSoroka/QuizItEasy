using System.Text.Json;

namespace QuizItEasy.Application.Common.Utility;

public static class JsonDefaults
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}
