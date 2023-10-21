using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CryptoUtils
{
    public static int pow(int a, int n)
    {
        int res = 1;
        for (int i = 0; i < n; i++)
        {
            res *= a;
        }
        return res;
    }
    public static int modexp(int x, int y, int N)
    {
        if (y == 0) return 1;
        int z = modexp(x, y / 2, N);
        if (y % 2 == 0)
            return (z * z) % N;
        else
            return ((x % N) * ((z * z) % N)) % N;
    }
    public static int gcd(int a, int b)
    {
        if (b == 0) return a;
        else return gcd(b, a % b);
    }
    static long jakobi(long a, long n)
    {
        if (n <= 0 || n % 2 == 0)
            return 0;

        long ans = 1L;

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

            long temp = a;
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
    public static bool Solovey_ShtrassenTest(int number)
    {
        if (number <= 2) return false;
        if (number % 2 == 0) return false;
        for (int k = 0; k < 10; k++)
        {
            int a = Random.Shared.Next(2, number);
            if (gcd(a, number) > 1)
            {
                return false;
            }
            int mod = modexp(a, (number - 1) / 2, number);
            int jak = (int)(number + jakobi(a, number)) % number;
            if (mod != jak)
            {
                return false;
            }
        }

        return true;
    }
    public static bool FermaTest(int number)
    {
        if (number <= 2) return false;
        if (number % 2 == 0) return false;
        for (int k = 0; k < 10; k++)
        {
            int a = Random.Shared.Next(2, number);
            if (modexp(a, number - 1, number) != 1) return false;
        }

        return true;
    }
    public static bool LemanTest(int number)
    {
        if (number <= 2) return false;
        if (number % 2 == 0) return false;
        for (int k = 0; k < 10; k++)
        {
            int a = Random.Shared.Next(2, number);
            int x = modexp(a, (number - 1) / 2, number);
            if (x != 1 && x != -1 && x != (number - 1)) return false;
        }

        return true;
    }
    public struct S_D
    {
        public int s;
        public int d;
    }
    public static S_D getS_D(int number)
    {
        int S = 0;
        int D = number;
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
    public static bool RabinTest(int number)
    {
        if (number <= 2) return false;
        if (number % 2 == 0) return false;
        var SD = getS_D(number);
        int s = SD.s; int d = SD.d;

        for (int k = 0; k < 10; k++)
        {
            int a = Random.Shared.Next(2, number);
            int x = modexp(a, (number - 1) / 2, number);
            if (x == 1 || x == -1 || x == (number - 1)) continue;
            for (int i = 0; i < s - 1; i++)
            {
                x = modexp(x, 2, number);
                if (x == number - 1) continue;
            }
            return false;
        }

        return true;
    }

    public static bool AllTest(int number)
    {
        return FermaTest(number) && LemanTest(number) && RabinTest(number) && Solovey_ShtrassenTest(number);
    }
}
