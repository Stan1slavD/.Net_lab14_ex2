﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GreatestCommonDivisor
{
    static class GCDAlgorithms
    {
        /// <summary>
        /// Find the lowest common divisor of two numbers using Euclid's Algorithm
        /// see: http://en.wikipedia.org/wiki/Euclidean_algorithm
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Lowest common divisor</returns>
        static public int FindGCDEuclid(int a, int b)
        {
            // If the first number is zero, return the second
            if (a == 0) return b;

            // Calculate the LCD
            while (b != 0)
            {
                if (a > b)
                {
                    a = a - b;
                }
                else
                {
                    b = b - a;
                }
            }

            return a;
        }

        // TODO Exercise 2, Task 2
        // Add overloaded methods for 3,4, and 5 integers
    }
}
