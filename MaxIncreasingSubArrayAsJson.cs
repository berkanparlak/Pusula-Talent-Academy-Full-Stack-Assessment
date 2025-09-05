using System;
using System.Collections.Generic;
using System.Text.Json;

public class MaxIncreasingSubArraySolution
{
    public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
            return JsonSerializer.Serialize(Array.Empty<int>());

        if (numbers.Count == 1)
            return JsonSerializer.Serialize(new int[] { numbers[0] });

        int bestStart = 0, bestLen = 1;
        long bestSum = numbers[0];

        int curStart = 0, curLen = 1;
        long curSum = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] > numbers[i - 1])
            {
                curLen++;
                curSum += numbers[i];
            }
            else
            {
                if (curSum > bestSum)
                {
                    bestSum = curSum;
                    bestStart = curStart;
                    bestLen = curLen;
                }
                curStart = i; curLen = 1; curSum = numbers[i];
            }
        }

        if (curSum > bestSum)
        {
            bestSum = curSum;
            bestStart = curStart;
            bestLen = curLen;
        }

        var result = numbers.GetRange(bestStart, bestLen);
        return JsonSerializer.Serialize(result);
    }
}
