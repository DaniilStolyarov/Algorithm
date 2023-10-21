using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

class CryptoUtils
{   
    
    public static BigInteger modexp(BigInteger x, BigInteger y, BigInteger N)
    {
        if (y == 0) return 1;
        BigInteger z = modexp(x, y / 2, N);
        if (y % 2 == 0)
            return (z * z) % N;
        else
            return ((x % N) * ((z * z) % N)) % N;
    }
    public static BigInteger gcd(BigInteger a, BigInteger b)
    {
        if (b == 0) return a;
        else return gcd(b, a % b);
    }
    static BigInteger jakobi(BigInteger a, BigInteger n)
    {
        if (n <= 0 || n % 2 == 0)
            return 0;

        BigInteger ans = 1L;

        if (a < 0)
        {
            a = -a; // (a/n) = (-a/n)*(-1/n)  
            if (n % 4 == 3)
                ans = -ans; // (-1/n) = -1 if n = 3 (mod 4)  
        }

        if (a == 1)
            return ans; // (1/n) = 1 

        while (a != 0)
        {
            if (a < 0)
            {
                a = -a; // (a/n) = (-a/n)*(-1/n)  
                if (n % 4 == 3)
                    ans = -ans; // (-1/n) = -1 if n = 3 (mod 4)  
            }

            while (a % 2 == 0)
            {
                a /= 2;
                if (n % 8 == 3 || n % 8 == 5)
                    ans = -ans;
            }

            BigInteger temp = a;
            a = n;
            n = temp;

            if (a % 4 == 3 && n % 4 == 3)
                ans = -ans;

            a %= n;
            if (a > n / 2)
                a = a - n;
        }

        if (n == 1)
            return ans;

        return 0;
    }
    /// <summary>
    /// checks if a number is a prime
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool Solovey_ShtrassenTest(BigInteger number)
    {
        if (number <= 2) return false;
        if (number % 2 == 0) return false;
        for (BigInteger k = 0; k < 10; k++)
        {
            var rng = RandomNumberGenerator.Create();
            BigInteger a = BigIntegerUtils.RandomInRange(rng, 2, number - 1);
            
            if (gcd(a, number) > 1)
            {
                return false;
            }
            BigInteger mod = modexp(a, (number - 1) / 2, number);
            BigInteger jak = (BigInteger)(number + jakobi(a, number)) % number;
            if (mod != jak)
            {
                return false;
            }
        }

        return true;
    }
    public static bool FermaTest(BigInteger number)
    {
        if (number <= 2) return false;
        if (number % 2 == 0) return false;
        for (BigInteger k = 0; k < 10; k++)
        {
            var rng = RandomNumberGenerator.Create();
            BigInteger a = BigIntegerUtils.RandomInRange(rng, 2, number - 1);
            if (modexp(a, number - 1, number) != 1) return false;
        }

        return true;
    }
    public static bool LemanTest(BigInteger number)
    {
        if (number <= 2) return false;
        if (number % 2 == 0) return false;
        for (BigInteger k = 0; k < 10; k++)
        {
            var rng = RandomNumberGenerator.Create();
            BigInteger a = BigIntegerUtils.RandomInRange(rng, 2, number - 1);
            BigInteger x = modexp(a, (number - 1) / 2, number);
            if (x != 1 && x != -1 && x != (number - 1)) return false;
        }

        return true;
    }
    public struct S_D
    {
        public BigInteger s;
        public BigInteger d;
    }
    public static S_D getS_D(BigInteger number)
    {
        BigInteger S = 0;
        BigInteger D = number;
        while (D % 2 == 0)
        {
            D /= 2;
            S += 1;
        }
        return new S_D{s=S, d=D};
    }
    /// <summary>
    /// https://ru.wikipedia.org/wiki/Тест_Миллера_—_Рабина
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool RabinTest(BigInteger number)
    {
        if (number <= 2) return false;
        if (number % 2 == 0) return false;
        var SD = getS_D(number);
        BigInteger s = SD.s; BigInteger d = SD.d;

        for (BigInteger k = 0; k < 10; k++)
        {
            var rng = RandomNumberGenerator.Create();
            BigInteger a = BigIntegerUtils.RandomInRange(rng, 2, number - 1);
            BigInteger x = modexp(a, (number - 1) / 2, number);
            if (x == 1 || x == -1 || x == (number - 1)) continue;
            for (BigInteger i = 0; i < s - 1; i++)
            {
                x = modexp(x, 2, number);
                if (x == number - 1) continue;
            }
            return false;
        }

        return true;
    }

    public static bool AllTest(BigInteger number)
    {
        return FermaTest(number) && LemanTest(number) && RabinTest(number) && Solovey_ShtrassenTest(number);
    }
}
