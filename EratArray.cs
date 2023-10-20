using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Getting primes less or equal N
/// </summary>
class EratArray
{
    public int[] array;

    public void print()
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
    }
    public EratArray(int number)
    {

        int[] _array = new int[number - 1];
        for (int i = 0; i < _array.Length; i++)
        {
            _array[i] = i + 2;
        }
        for (int i = 0; i < _array.Length; i++)
        {
            if (_array[i] == 0)
            {
                continue;
            }

            for (int j = _array[i] * 2; j < _array.Length + 2; j += _array[i])
            {
                _array[j - 2] = 0;
            }
        }

        int prefix = 0;
        for (int i = 0; i < _array.Length; i++)
        {
            if (_array[i] != 0)
            {
                prefix++;
            }
        }
        array = new int[prefix];
        prefix = 0;
        for (int i = 0; i < _array.Length; i++)
        {
            if (_array[i] != 0)
            {
                array[prefix] = _array[i];
                prefix++;
            }
        }
    }
}
