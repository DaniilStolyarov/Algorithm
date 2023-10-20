using System;
using System.Collections;
using System.Numerics;

struct Range
{
    public int start;
    public int end;
    public CompositeNumber[] globalMap;
    public Range(int start, int end, CompositeNumber[] _globalMap)
    {
        this.start = start;
        this.end = end;
        this.globalMap = _globalMap;
    }
}


class myProgram
{
    static async Task partCalc(Range range)
    {
        Console.WriteLine("start: " + range.start + " end: " + range.end);
        await Task.Run(() =>
        {
            for (int i = range.start; i < range.end; i++)
            {
                CompositeNumber CN = null;
                try
                {
                    CN = new CompositeNumber((ulong)i);
                }
                catch (Exception e)
                {
                    Console.WriteLine("error with " + i);
                    return;
                }
                range.globalMap[CN.value] = CN;
                /*Console.Write($"{i} factors: ");
                CN.printFactors();
                Console.WriteLine();*/
            }
        });
    }
    static void task1()
    {
        /*var CN = new CompositeNumber(160001);
CN.printFactors();*/

        const int maxTasks = 12;
        const int max = 252 * 1000;
        Task[] Tasks = new Task[maxTasks];
        CompositeNumber[] globalList = new CompositeNumber[max];
        for (int i = 0; i < maxTasks; i++)
        {
            var task = partCalc(new Range((int)((double)max / maxTasks) * i + (i == 0 ? 2 : 0), (int)((double)max / maxTasks) * (i + 1), globalList));
            Tasks[i] = task;
            // partCalc(new Range((int)((double)max / maxTasks) * i + (i == 0 ? 2 : 0), (int)((double)max / maxTasks) * (i + 1), globalList))
        }
        Task.WaitAll(Tasks);

        Console.WriteLine(globalList.Length);
        for (int i = 2; i < globalList.Length; i++)
        {

            Console.Write($"{i}\t");
            if (globalList[i] == null)
            {
                Console.Write("null");
            }
            else globalList[i].printFactors();
            Console.WriteLine();
        }

    }
    static void Main(string[] args)
    {
        task1();
    }
}
