// Exercise 4.1
(*9.1 Consider the function g declared on Page 202 and the stack and heap after the evaluation of g 2 shown in Figure 9.2.
Reproduce this resulting stack and heap by a systematic application of push and pop operations on the stack, and heap  allocations that follow the step by step evaluation of g 2.*)
//          Stack       Heap





// Exercise 4.2
// 9.3 Declare an iterative solution to exercise 1.6.
// 1.6 Declare a recursive function sum: int * int -> int, where
//      sum (m,n) = m + (m + 1) + (m + 2) + ··· + (m + (n − 1)) + (m + n)
//      for m ≥ 0 and n ≥ 0 . (Hint: use two clauses with (m,0) and (m,n) as patterns.)
//      Give the recursion formula corresponding to the declaration.
// let rec sum' s = function 
//     | (m, n) when m < 0 || n < 0 -> failwith "Invalid argument"
//     | (m, 0) -> s
//     | (m, n) -> sum' (s + m + n) (m, n - 1)

let rec sum' s (m, n) = if m < 0 || n < 0 then failwith "Invalid argument"
                        elif n = 0 then s
                        else sum' (s + m + n) (m, n - 1)
let sum = sum' 0

sum (1, 5) // 20

// let sum2 (m, n) = m * n + (n * (n + 1)) / 2
// sum2 (0, 99) // 4950

// Exercise 4.3
// HR exercise 9.4.
// Give iterative declarations of the list function List.length.
// One iterative declaration is enough

let rec le (xs, l) = if not <| List.isEmpty xs
                     then let _::xs' = xs in le (xs', l + 1) 
                     else l    
let length xs = le (xs, 0)

length [1..100]     // 100

// Exercise 4.4
// HR exercise 9.6
// Declare a continuation-based version of the factorial function and compare the run time with
//  the results in Section 9.4.
let rec fact' n c = if n = 1 then c 1 else fact' (n-1) (fun x -> c(x*n))
let fact n = fact' n id

fact 16 // 2004189184

let xs16 = List.init 1000000 (fun i -> 16)

#time 

for i in xs16 do fact i |> ignore // Real: 00:00:00.192, CPU: 00:00:00.187, GC gen0: 318, gen1: 0, gen2: 0

// Slower than factA by a factor 10 taking into account the time for the loop 



// Exercise 4.5
// HR exercise 8.6
// This to be used in the next task.
// Declare a function for computing Fibonacci numbers F n (see Exercise 1.5) using a while
// loop. Hint: introduce variables to contain the two previously computed Fibonacci numbers.

let fib n = 
    if n = 0 then 0
    elif n = 1 then 1
    else
        let mutable n1 = 0
        let mutable n2 = 1
        let mutable i = 2
        while i < n do
            let tmp = n2
            n2 <- n1 + n2
            n1 <- tmp
            i <- i + 1
        n1 + n2

fib 15 // 610




// Exercise 4.6
// HR exercise 9.7
//  Develop the following three versions of functions computing Fibonacci numbers F n (see Exer-
//  cise 1.5):

// 1. A version fibA: int -> int -> int -> int with two accumulating parameters n1 and
// n2 , where fibA n n1 n2 = F n , when n1 = F n−1 and n2 = F n−2 . Hint: consider suitable
// definitions of F −1 and F −2 .

let rec fibA n1 n2 n = if n = 0 then n2 else fibA (n1 + n2) n1 (n-1)

fibA 1 0 15 // 610
  
// 2. A continuation-based version fibC: int -> (int -> int) -> int that is based on the
// definition of F n given in Exercise 1.5.

// slow1
// let rec fibC n c = if n = 0 then c 0 
//                    elif n = 1 then c 1 
//                    else fibC (n-1) (fun n1 -> fibC (n-2) (fun n2 -> c(n2 + n1)))

// Slow2
let rec fibC n c = if n = 0 then c 0 
                   elif n = 1 then c 1
                   else fibC (n-1) (fun res -> c(res + fibC (n-2) id))

fibC 15 id // 610

// Compare these two functions using the directive #time, and compare this with the while-loop
// based solution of Exercise 8.6.

#time
for i in [1..10000000] do fib 46 // Real: 00:00:01.238, CPU: 00:00:01.250, GC gen0: 53, gen1: 27, gen2: 2

for i in [1..10000000] do fibA 1 0 46 // Real: 00:00:01.445, CPU: 00:00:01.468, GC gen0: 53, gen1: 27, gen2: 2

fibC 46 id // Real: 00:00:46.304, CPU: 00:00:46.140, GC gen0: 52893, gen1: 5, gen2: 0

for i in [1..10000000] do () // Real: 00:00:01.288, CPU: 00:00:01.390, GC gen0: 53, gen1: 28, gen2: 2

// Answer:
//      The while and accumulation based functions seems to be pretty much instant, 
//      running the code 10.000.000 times, the majority of the time is spent on the loop itself
//      The continuation based version seems to be 10^7 times slower, which seems pretty extreme 
//      There might be a problem with the way I've defined it


// Exercise 4.7
// HR exercise 9.8
type BinTree<'a> = 
    | Leaf
    | Node of BinTree<'a> * 'a * BinTree<'a>

let rec countA s = function
    | Leaf -> s
    | Node(l, _, r) -> countA (countA (s + 1) l) r

let t3 = Node(Node(Leaf, -3, Leaf), 0, Node(Leaf, 2, Leaf));;
let t4 = Node(t3, 5, Node(Leaf, 7, Leaf));;

countA 0 t3 // 3
countA 0 t4 // 5

// Exercise 4.8
// HR exercise 9.9

// ?????????

// let rec countAC t a c = 
//     match t with
//     | Leaf -> 1
//     | Node(l, _, r) -> countAC l a (fun a -> a + 1)

// countAC t3 0 id

// Exercise 4.9
// HR exercise 9.10
// Consider the following list-generating function
// let rec bigListK n k =
//     if n=0 then k []
//     else bigListK (n-1) (fun res -> 1::k(res));;

// The call bigListK 130000 id causes a stack overflow. Analyze this problem.

// Answer:
//      Since 1::k(res), is not tail-recursive, it builds up a giant stack to calculate the result of k(res)
//      because it's not possible to append 1 to the list before k(res) has been evaluated 

// Exercise 4.10
// HR exercise 9.11
// Declare tail-recursive functions leftTree and rightTree. By use of leftTree it should
// be possible to generate a big unbalanced tree to the left containing n + 1 values in the nodes so
// that n is the value in the root, n − 1 is the value in the root of the left subtree, and so on. All
// subtree to the right are leaves. Similarly, using rightTree it should be possible to generate a
// big unbalanced tree to the right.

// 1. Use these functions to show the stack limit when using count and countA from Exercise 9.8.

// 2. Use these functions to test the performance of countC and countAC from Exercise 9.9.


// Exercise 4.11
// HR exercise 11.1

let odd = Seq.initInfinite (fun x -> x * 2 + 1)
Seq.item 5 odd // 11

// Exercise 4.12
// HR exercise 11.2

let factSeq = Seq.initInfinite fact
Seq.item 5 factSeq