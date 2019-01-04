using System;

public class Test
{
    public Test(int seqBufLength)
    {
        seqBuf = new uint[seqBufLength];
    }

    public uint[] seqBuf;

    public void SeqBufClean(ushort from, ushort to, uint defValue)
    {
        var from32 = (uint) from;
        var to32 = (uint) to;
        if (to32 < from32)
        {
            to32 += (uint) (64 * seqBuf.Length);
            Console.WriteLine($"to32 = {to32}");
        }

        var needFill = (to32 - from32) % seqBuf.Length + 1;
        Console.WriteLine($"needFill = {needFill}");
        if (needFill == seqBuf.Length)
        {
            SeqBufReset(defValue);
        }
        else
        {
            for (var seqId = from32; needFill > 0; seqId++)
            {
                seqBuf[seqId % seqBuf.Length] = defValue;
                needFill--;
            }
        }
    }

    public void SeqBufReset(uint defValue)
    {
        Console.WriteLine("resetting all");
        if (seqBuf.Length == 0)
        {
            return;
        }

        seqBuf[0] = defValue;
        for (var bp = 1; bp < seqBuf.Length; bp *= 2)
        {
            Array.Copy(seqBuf, 0, seqBuf, bp, Math.Min(seqBuf.Length - bp, bp));
        }
    }

    public override string ToString()
    {
        string result = "";
        for (var i = 0; i < seqBuf.Length; i++)
        {
            if (i == 0)
            {
                result = "[";
            }
            else
            {
                result += " ";
            }

            result += seqBuf[i];
            if (i == seqBuf.Length - 1)
            {
                result += "]";
            }
        }

        return result;
    }


    public static void Main(string[] args)
    {
        var obj = new Test(1024);
        Console.WriteLine($"seqBuf.Length: {obj.seqBuf.Length}");

        Console.WriteLine(obj);

        obj.SeqBufReset(1);

        Console.WriteLine($"SeqBufReset(1): {obj}");
        obj.SeqBufClean(0, 0, 2);
        Console.WriteLine($"SeqBufClean(0,0,2): {obj}");
        obj.SeqBufClean(0, 0, 3);
        Console.WriteLine($"SeqBufClean(0,0,3): {obj}");
        obj.SeqBufClean(0, 1, 4);
        Console.WriteLine($"SeqBufClean(0,1,4): {obj}");
        obj.SeqBufClean(2, 4, 5);
        Console.WriteLine($"SeqBufClean(2,4,5): {obj}");
        obj.SeqBufClean(6, (ushort) (obj.seqBuf.Length - 1), 6);
        Console.WriteLine($"SeqBufClean(6,obj.seqBuf.Length-1,6): {obj}");
        obj.SeqBufClean(7, 0, 7);
        Console.WriteLine($"SeqBufClean(7, 0, 7): {obj}");
        obj.SeqBufClean(8, 6, 8);
        Console.WriteLine($"SeqBufClean(8, 6, 8): {obj}");
        obj.SeqBufClean(9, 8, 9);
        Console.WriteLine($"SeqBufClean(9, 8, 9): {obj}");
        obj.SeqBufClean(10, 10, 10);
        Console.WriteLine($"SeqBufClean(10, 10, 10): {obj}");
        obj.SeqBufClean(19, 0, 11);
        Console.WriteLine($"SeqBufClean(19, 0, 11): {obj}");

        obj.SeqBufClean(20, 18, 12);
        Console.WriteLine($"SeqBufClean(20, 18, 12): {obj}");


        obj.SeqBufClean(24, 20, 0);
        Console.WriteLine($"SeqBufClean(24, 20, 0): {obj}");

        obj.SeqBufClean(20, 24, 1);
        Console.WriteLine($"SeqBufClean(20, 24, 1): {obj}");
    }
}