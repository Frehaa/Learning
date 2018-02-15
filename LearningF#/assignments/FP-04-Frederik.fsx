// Exercise 4.1
(*9.1 Consider the function g declared on Page 202 and the stack and heap after the evaluation of g 2 shown in Figure 9.2.
Reproduce this resulting stack and heap by a systematic application of push and pop operations on the stack, and heap  allocations that follow the step by step evaluation of g 2.*)
//              Stack                       Heap
//  sf0     









// Exercise 4.2
// 9.3 Declare an iterative solution to exercise 1.6.
// 1.6 Declare a recursive function sum: int * int -> int, where
//      sum (m,n) = m + (m + 1) + (m + 2) + ··· + (m + (n − 1)) + (m + n)
//      for m ≥ 0 and n ≥ 0 . (Hint: use two clauses with (m,0) and (m,n) as patterns.)
//      Give the recursion formula corresponding to the declaration.
let rec sum' s = function 
    | (m, n) when m < 0 || n < 0 -> failwith "Invalid argument"
    | (m, 0) -> s
    | (m, n) -> sum' (s + m + n) (m, n - 1)

let sum = sum' 0

sum (1, 5) // 20

// let sum2 (m, n) = m * n + (n * (n + 1)) / 2
// sum2 (0, 99) // 4950

// Exercise 4.3
// HR exercise 9.4.
// One iterative declaration is enough.

let rec length (xs, l) = if not (List.isEmpty xs) then let _::xs' = xs in length (xs', l + 1) else l

length ([1..100], 0) // 100

// Exercise 4.4
// HR exercise 9.6

// Exercise 4.5
// HR exercise 8.6
// This to be used in the next task.
// Exercise 4.6
// HR exercise 9.7
// Exercise 4.7
// HR exercise 9.8
// Exercise 4.8
// HR exercise 9.9
// Exercise 4.9
// HR exercise 9.10
// Exercise 4.10
// HR exercise 9.11
// Exercise 4.11
// HR exercise 11.1
// Exercise 4.12
// HR exercise 11.2