(* Exercise 1.1: Write a function sqr:int->int so that sqr x returns x^2 *)
let sqr x = x * x

sqr 5 // 25

(* Exercise 1.2: Write a function pow :  float -> float -> float so that pow x n returns x^n. You can use the library function: System.Math.Pow. *)
let rec pow x n = 
    match n with
    | 0 -> 1.0
    | b -> x * pow x (n-1)

pow 3.0 3 // 27.0

(* Exercise 1.3: Solve HR, exercise 1.1*)
let g n = n + 4

g 4 // 8

(* Exercise 1.4: Solve HR, exercise 1.2 *)
let h (x, y) = System.Math.Sqrt(x ** 2.0 + y ** 2.0)

h (3.0, 4.0) // 5.0

(* Exercise 1.5: Solve HR, exercise 1.4 *)
let rec f = function
| 0 -> 0
| n -> n + f (n-1) 

f 10 // 55

(* Exercise 1.6: Solve HR, exercise 1.5 *)
let rec fib = function
| 0 -> 0
| 1 -> 1
| n -> fib (n-1) + fib (n-2)

fib 4 // 3

(* Exercise 1.7: Solve HR, exercise 1.6 *)
let rec sum = function
| (m, 0) -> m
| (m, n) -> m + n + sum (m, n - 1)

// sum (m, n) = m + n + sum (m, n - 1)

sum (0, 10) // 55
sum (1, 3) // 10

(* Exercise 1.8: Solve HR, exercise 1.7 *)
// (System.Math.PI, fact -1) : (float * int -> int)
// fact(fact 4) : int
// power(System.Math.PI, fact 2) : float
// (power, fact) : ((float -> int -> float) * (int -> int))

(* Exercise 1.9: Solve HR, exercise 1.8 *)
// let a = 5;;
// let f a = a + 1
// let g b = (f b) + a

// Env = a -> 5
//       f -> "Adds one method"
//       g -> "Uses f and adds a, method"

// g 3
// ~> f n + a [b -> 3, a -> 5]
// ~> f 3 + 5
// ~> (3 + 1) + 5
// ~> 4 + 5
// ~> 9

// f 3
// ~> n + 1 [n -> 3]
// ~> 3 + 1
// ~> 4

(* Exercise 1.10: Write a function dup:string->string that concatenates a string with itself. You can either use + or ˆ. For example: *)
// val dup : string -> string
// > dup "Hi ";;
// val it : string = "Hi Hi "

let dub s = s + s

dub "Hi "

(* Exercise 1.11: Write a function dupn:string->int->string so that dupn s n creates the concatenation of n copies of s. For example: *)
// val dupn : string -> int -> string
// > dupn "Hi " 3;;
// val it : string = "Hi Hi Hi "

let rec dupn s n = match n with
| 0 -> ""
| n -> s + dupn s (n-1)

(* Exercise 1.12: Assume the time of day is represented as a pair(hh, mm):int * int.
                   Write a function timediff:int * int->int * int->int so that timediff t1 t2 computes the difference in minutes between t1 and t2, i.e., t2-t1. 
                   A few examples  *)
// val timediff : int * int -> int * int -> int
// > timediff (12,34) (11,35);;
// val it : int = -59
// > timediff (12,34) (13,35);;
// val it : int = 61

let timediff (hh1, mm1) (hh2, mm2) = 
        let hdif = (hh2 - hh1)
        hdif * 60 + mm2 - mm1;;
                                     
timediff (12,34) (11,35);; // -59
timediff (12,34) (13,35);; // 61
                                   

(* Exercise 1.13: Write a function minutes:int * int->int to compute the number of minutes since midnight. 
   Easily done using the function timediff. A few examples: *)
// val minutes : int * int -> int
// > minutes (14,24);;
// val it : int = 864
// > minutes (23,1);;
// val it : int = 1381

let minutes x = timediff (24, 00) x |> (fun x -> if x < 0 then 60 * 24 - x else x);;


timediff (24, 00) (14, 24)
minutes (24, 55);;
minutes (14,24);;
minutes (23,1);;

(* Exercise 1.14 Solve HR, exercise 2.2 *)
(* Exercise 1.15 Solve HR, exercise 2.8 *)
(* Exercise 1.16 Solve HR, exercise 2.9 *)
(* Exercise 1.17 Solve HR, exercise 2.10 *)
(* Exercise 1.18 Solve HR, exercise 2.13 *)


