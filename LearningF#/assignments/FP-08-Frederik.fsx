// IT UNIVERSITY BFNP SPRING 2018

// Assignment 8 for Tuesday 20 March 2018
// Version 1.0 of 2018-03-01 sestoft@itu.dk


// These exercises concern parallel programming in F#.

// You should build on the lecture's example code, found in file
// http://www.itu.dk/people/sestoft/bachelor/bfnp20180320.zip 


// Exercise 1. Run the slow Fibonacci computations from the lecture's
// examples on your own computer.  Use the #time directive to turn on
// timing, and see what is the best speed-up you can obtain for
// computing, say, the first 43 Fibonacci numbers using Async.Parallel.
// This may be quite different on MS .NET than on Mono.
let rec fib = function
| 0 -> 0
| 1 -> 1
| n -> fib (n-1) + fib (n-2);;

#time
Array.map fib [|1..43|] // Real: 00:00:08.769, CPU: 00:00:08.218, GC gen0: 0, gen1: 0, gen2: 0
Array.Parallel.map fib [|1..43|] // Real: 00:00:05.242, CPU: 00:00:08.156, GC gen0: 0, gen1: 0, gen2: 0


// Exercise 2. Similarly, run the prime factorization example on your own
// computer, and see what speedup you get.  


// Exercise 3. The lecture's construction of a histogram (counting the
// numbers of times that each prime factor 2, 3, 5, 7, 11 ... appears)
// uses a side effect in the assignment 

//      histogram.[i] <- histogram.[i] + 1 

// But side effects should be avoided.  Program the histogram
// construction so that it does not use side effects but purely
// functional programming.  There are several useful functions in the Seq
// module.  The final result does not have to be an int[] array, but
// could be a seq<int * int> of pairs (p, n) where p is a prime factor
// and n is the number of times p appears in the array of lists of prime
// factors.


// Exercise 4. Find the fastest way on your hardware to count the number
// of prime numbers between 1 and 10 million (the correct count is
// 664579).  Use this F# function to determine whether a given number n
// is prime:


#time 
let isPrime n =
    let rec testDiv a = a*a > n || n % a <> 0 && testDiv (a+1)
    n>=2 && testDiv 2;;

#time

let (v, _) = Array.Parallel.partition isPrime [|2..10_000_000|]
Array.length v // 664579 - Real: 00:00:01.564, CPU: 00:00:05.171, GC gen0: 1, gen1: 0, gen2: 0

// or if you believe it would be faster in C, C# or Java, you may use
// this version:

// private static boolean isPrime(int n) {
//   int k = 2;
//   while (k * k <= n && n % k != 0)
//     k++;
//   return n >= 2 && k * k > n;
// }

// Remember to use parallel programming to the extent possible.  On my
// Intel i7 Mac the result can be computed in 1.85 seconds wall-clock
// time using 4 cores, and 80 characters of F# code (in addition to the
// above function).
