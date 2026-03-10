using System;
using System.Collections.Generic;

namespace PrimeNumber
{
    class Program
    {
       
        static bool IsPrime(int num)
        {
            if (num <= 1) return false; 
            if (num == 2) return true; 
            if (num % 2 == 0) return false; 

            
            for (int i = 3; i * i <= num; i += 2)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            try
            {
                
                Console.Write("请输入下限：");
                int lower = int.Parse(Console.ReadLine());

                Console.Write("请输入上限：");
                int upper = int.Parse(Console.ReadLine());

                
                if (lower > upper)
                {
                    int temp = lower;
                    lower = upper;
                    upper = temp;
                    Console.WriteLine($"已自动交换上下限，范围为：{lower} ~ {upper}");
                }

                
                List<int> primes = new List<int>();
                for (int i = lower; i <= upper; i++)
                {
                    if (IsPrime(i))
                    {
                        primes.Add(i);
                    }
                }

                
                Console.WriteLine($"\n在 {lower} 和 {upper} 之间的素数有：");
                if (primes.Count == 0)
                {
                    Console.WriteLine("该范围内没有素数。");
                }
                else
                {
                    for (int i = 0; i < primes.Count; i++)
                    {
                        Console.Write(primes[i] + "\t");
                        
                        if ((i + 1) % 10 == 0)
                        {
                            Console.WriteLine();
                        }
                    }
                    
                    if (primes.Count % 10 != 0)
                    {
                        Console.WriteLine();
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("输入错误，请输入整数！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误：{ex.Message}");
            }

            
            Console.ReadKey();
        }
    }
}