using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class LongestVowelSubsequenceSolution
{
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        if (words == null || words.Count == 0)
            return JsonSerializer.Serialize(new List<object>());

        var vowels = new HashSet<char>("aeiouAEIOUıİöÖüÜ");

        var results = new List<object>(words.Count);

        foreach (var word in words)
        {
            if (string.IsNullOrEmpty(word))
            {
                results.Add(new { word = word ?? "", sequence = "", length = 0 });
                continue;
            }

            int bestStart = -1, bestLen = 0;
            int curStart = -1, curLen = 0;

            for (int i = 0; i < word.Length; i++)
            {
                if (vowels.Contains(word[i]))
                {
                    if (curLen == 0) curStart = i;
                    curLen++;

                    if (curLen > bestLen)
                    {
                        bestLen = curLen;
                        bestStart = curStart;
                    }
                }
                else
                {
                    curLen = 0;
                }
            }

            string seq = (bestLen > 0 && bestStart >= 0) ? word.Substring(bestStart, bestLen) : "";
            results.Add(new { word, sequence = seq, length = seq.Length });
        }

        return JsonSerializer.Serialize(results);
    }
}
