using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// A class for factorization by Ferma algorithm
/// </summary>
internal class CompositeNumber
{
    /// <summary>
    /// number to factorize
    /// </summary>
    public ulong value { get; }
    /// <summary>
    /// list for collecting factors of number
    /// </summary>
    public List<ulong> factors = new List<ulong>();
    /// <summary>
    /// Gets a value and fills factors list
    /// </summary>
    /// <param name="_value">number to factorize. Must be more than 2</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    public CompositeNumber(ulong _value)
    {
        if (_value < 2)
        {
            throw new ArgumentException("value of CompositeNumber cannot be less than 2");
        }
        else if (_value < 4)
        {
            factors.Add(_value);
            value = _value;
            return;
        }
        value = _value;
        FermaRecursion(_value);
        factors.Sort();
        if (!isCorrect())
        {
            throw new Exception("not worked with number: " + value);
        }
    }

    public void printFactors()
    {
        foreach (ulong entry in factors)
        {
            Console.Write(entry + " ");
        }
    }
    /// <summary>
    /// Recursively applies Ferma() method to number and to the results of it. <br></br>
    /// Before Ferma() is called, it verifies that a number is not EVEN and not a SQUARE.
    /// </summary>
    /// <param name="number"></param>
    public void FermaRecursion(ulong number)
    {
        ulong temp = number;
        while (isSquare(temp))
        {
            temp = (ulong)Math.Sqrt(temp);
            FermaRecursion(temp);
        }
        while (temp % 2 == 0)
        {
            temp = temp / 2;
            factors.Add(2);
        }
        if (temp == 1) return;
        ulong[] two_factors = Ferma(temp);

        if (two_factors[0] == 1 && two_factors[1] == temp)
        {
            factors.Add(two_factors[1]);
            return;
        }

        FermaRecursion(two_factors[0]); FermaRecursion(two_factors[1]);

    }
    /// <summary>
    /// gets a number and returns it`s two factors. <br></br>
    /// If the number is prime, returns {1, number} factors.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static ulong[] Ferma(ulong number)
    {
        // number - нечетное число, которое не является квадратом
        ulong x = (ulong)Math.Floor(Math.Sqrt(number));
        ulong k = 1;
        for (ulong i = 0; i < number / 2; i++)
        {
            ulong y_s = (x + k) * (x + k) - number;

            if (isSquare(y_s))
            {
                return new ulong[] { (x + k) - (ulong)Math.Sqrt(y_s), (x + k) + (ulong)Math.Sqrt(y_s) };
            }
            k++;
        }
        return new ulong[] { 1, number };
    }
    /// <summary>
    /// checks if a number is a Square
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool isSquare(ulong number)
    {
        return ((ulong)Math.Sqrt(number) * (ulong)Math.Sqrt(number)) == number;
    }
    /// <summary>
    /// checks if factorization was correct. If not, returns false.
    /// </summary>
    /// <returns></returns>
    private bool isCorrect()
    {
        ulong number = 1;
        foreach (var entry in factors)
        {
            number *= entry;
        }
        return number == this.value;
    }
}
