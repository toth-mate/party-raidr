namespace PartyRaidR.Backend.Converters
{
    public static class ConverterHelper
    {
        public static IEnumerable<TOut> ConvertAll<TIn, TOut>(IEnumerable<TIn> models, Func<TIn, TOut> converter) =>
            models.Select(converter);
    }
}
