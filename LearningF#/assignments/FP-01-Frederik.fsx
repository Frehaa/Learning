(* Exercise 1.1: Write a function sqr:int->int so that sqr x returns x^2 *)
let sqr x = x * x;;

sqr 5;; // 25

(* Exercise 1.2: Write a function pow :  float -> float -> float so that pow x n returns x^n. You can use the library function: System.Math.Pow. *)
let rec pow x n = 
    match n with
    | 0 -> 1.0
    | n -> x * pow x (n-1);;

pow 3.0 3;; // 27.0

(* Exercise 1.3: Solve HR, exercise 1.1*)
let g n = n + 4;;

g 4;; // 8

(* Exercise 1.4: Solve HR, exercise 1.2 *)
let h (x, y) = System.Math.Sqrt(x ** 2.0 + y ** 2.0);;

h (3.0, 4.0) // 5.0

(* Exercise 1.5: Solve HR, exercise 1.4 *)
let rec f = function
| 0 -> 0
| n -> n + f (n-1) ;;

f 10;; // 55

(* Exercise 1.6: Solve HR, exercise 1.5 *)
let rec fib = function
| 0 -> 0
| 1 -> 1
| n -> fib (n-1) + fib (n-2);;

fib 4;; // 3

(* Exercise 1.7: Solve HR, exercise 1.6 *)
let rec sum = function
| (m, 0) -> m
| (m, n) -> m + n + sum (m, n - 1);;

// sum (m, n) = m + n + sum (m, n - 1)

sum (0, 10);; // 55
sum (1, 3);; // 10

(* Exercise 1.8: Solve HR, exercise 1.7 *)
// (System.Math.PI, fact -1) : (float * int -> int)
// fact(fact 4) : int
// power(System.Math.PI, fact 2) : float
// (power, fact) : ((float -> int -> float) * (int -> int))

(* Exercise 1.9: Solve HR, exercise 1.8 *)
// let a = 5;;
// let f a = a + 1
// let g b = (f b) + a

// Env = [a -> 5
//        f -> "Adds one method"
//        g -> "Uses f and adds a, method"]

// g 3
// ~> (f b) + a,  [b -> 3, a -> 5]
// ~> f 3 + 5
// ~> (a + 1) + 5, [a -> 3]
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

let dub s = s + s;;

dub "Hi ";;

(* Exercise 1.11: Write a function dupn:string->int->string so that dupn s n creates the concatenation of n copies of s. For example: *)
// val dupn : string -> int -> string
// > dupn "Hi " 3;;
// val it : string = "Hi Hi Hi "

let rec dupn s n = 
    match n with
    | 0 -> ""
    | n -> s + dupn s (n-1);;

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

let minutes x = timediff (24, 00) x |> (fun x -> if x < 0 then x + 60 * 24 else x);;

minutes (14,24);; // 864
minutes (23,1);; // 1381

(* Exercise 1.14 Solve HR, exercise 2.2 *)
let rec pow : (string * int) -> string = function
| (s, 1) -> s
| (s, n) -> s + pow(s, n-1);;

pow ("Test ", 3);; // "Test Test Test "

(* Exercise 1.15 Solve HR, exercise 2.8 *)
let rec bin = function 
| (n, k) when n < 0 || k < 0 || k > n -> failwith "Invalid argument"
| (n, k) when n = k -> 1
| (_, 0) -> 1
| (n, k) -> bin(n-1, k-1) + bin(n-1, k);;

bin (4, 2);; // 

(* Exercise 1.16 Solve HR, exercise 2.9 *)
// let rec f = function 
// | (0,y) -> y
// | (x,y) -> f (x-1, x*y);;

// 1)
//  f : int * int -> int

// 2)
// Teminates if first value in pair is >= 0

// 3)
// f(2,3);;
// -> f (x - 1, x * y) , [x -> 2, y -> 3]
// -> f (2 - 1, 2 * 3)
// -> f (2-1, 6)
// -> f (1, 6)
// -> f (x-1, x * y), [x -> 1, y -> 6]
// -> f (1-1, 1 * 6)
// -> f (0, 6)
// -> 6

// 4)
// f(x,y) = y * x!

(* Exercise 1.17 Solve HR, exercise 2.10 *)
// let test (c,e) = if c then e else 0;;

// 1) 
// test : bool * int -> int

// 2)
// Doesn't evaluate since fact -1 doesn't evaluate

// 3)
// Can evaluate since fact -1 doesn't get evaluated

(* Exercise 1.18 Solve HR, exercise 2.13 *)

// curry    : (’a * ’b -> ’c) -> ’a -> ’b -> ’c
let curry f a b = f(a, b);;

// uncurry  : (’a -> ’b -> ’c) -> ’a * ’b -> ’c
let uncurry f (a, b) = f a b;;

