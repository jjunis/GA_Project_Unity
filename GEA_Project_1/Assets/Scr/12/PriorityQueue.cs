using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<(T item, int priority)> elements = new List<(T, int)>();

    public int Count => elements.Count;

    public void Enqueue(T item, int priority)
    {
        elements.Add((item, priority));
    }

    public bool TryDequeue(out T item, out int priority)
    {
        if (elements.Count == 0)
        {
            item = default(T);
            priority = default(int);
            return false;
        }

        int bestIndex = 0;
        int bestPriority = elements[0].priority;

        for (int i = 1; i < elements.Count; i++)
        {
            if (elements[i].priority < bestPriority)
            {
                bestPriority = elements[i].priority;
                bestIndex = i;
            }
        }

        item = elements[bestIndex].item;
        priority = elements[bestIndex].priority;
        elements.RemoveAt(bestIndex);

        return true;
    }
}
