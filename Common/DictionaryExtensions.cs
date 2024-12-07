namespace Common;

public static class DictionaryExtensions
{
    public static TValue ComputeIfAbsent<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
    {
        if (dictionary.TryGetValue(key, out var value)) return value;

        var newValue = new TValue();
        dictionary.Add(key, newValue);
        return newValue;
    }
}
