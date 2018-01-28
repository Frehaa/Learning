(* 2.1 *)
let f n = (n % 2 = 0 || n % 3 = 0) && n % 5 <> 0;;

f 24;; // true
f 27;; // true
f 29;; // false
f 30;; // false


(* 2.2 *)
let rec pow : (string * int) -> string = function
| (s, 0) -> s
| (s, n) -> s + pow(s, n-1);;

pow ("Test ", 3);; // "Test Test Test Test "


(* 2.3 *)
let isIthChar (str:string, i:int, ch:char) = str.[i] = ch;;

isIthChar ("Testing", 5, 'n');; // true
isIthChar ("Hello", 2, 'e');; // false

(* 2.4 *)
let occFromIth (str:string, i:int, ch:char) = 
    let mutable j = i
    let mutable c = 0
    while j < str.Length do 
        if str.[j] = ch then c <- c + 1
        j <- j + 1
    c;;

occFromIth("testing", 1, 't')

(* 2.5 *)
let occInString (str:string, ch:char) = 
    let mutable c = 0
    for sc in str do
        if sc = ch then c <- c + 1
    c;;

occInString("Testing", 't');;


(* 2.6 *)
let notDivisible (d, n) = n % d <> 0;;

notDivisible (2, 5);;
notDivisible (3, 9);;

(* 2.7 *)

(* 1 *)
let rec test = function
| (a,b,c) when a = b -> notDivisible(b, c)
| (a,b,c) -> notDivisible(a, c) && test(a+1, b, c);;

test (2, 12, 13);;

(* 2 *)

let prime n = test(2, n-1, n);;
prime 97;;
prime 96;;

(* 3 *)
let rec nextPrime = function
| n when prime (n+1) -> n+1
| n -> nextPrime (n+1);;

nextPrime 16;;

(* 2.8 *)
let rec bin = function 
| (n, k) when n < 0 || k < 0 || k > n -> failwith "Invalid argument"
| (n, k) when n = k -> 1
| (n, 0) -> 1
| (n, k) -> bin(n-1, k-1) + bin(n-1, k);;


bin (0,0);;
bin (1,1);;
bin (1,0);;
bin (2,0);;
bin (2,1);;
bin (2,2);;
bin (3,0);;
bin (3,1);;
bin (3,2);;
bin (3,3);;
bin (4,0);;
bin (4,1);;
bin (4,2);;
bin (4,3);;
bin (4,4);;
bin (5,0);;
bin (5,1);;
bin (5,2);;
bin (5,3);;
bin (5,4);;
bin (5,5);;
// bin (2, -1);; ERROR


(* 2.9 *)
let rec f2 = function
| (0,y) -> y                // Terminates 
| (x,y) -> f2(x-1, x*y);;

f2(2, 3);;
// -> f2(2-1, 2*3)
// -> f2(2-1, 6)
// -> f2(1, 6)
// -> f2(1-1, 6)
// -> f2(0, 6)
// -> 6

// f2(x,y) = y * x!
f2(5, 1);;

(* 2.10 *)
let test2 (c,e) = if c then e else 0;;

// test2 (false, fact (-1)) Doesn't terminate

// let test3 (c, e) = if c then fact -1 else 0;;
// Can terminate if c = true

(* 2.11 *)
let VAT n x = float(n) * (1.0 + x);;
VAT 100 0.20;;

let unVAT n x = (x / float(n))-1.0;;

unVAT 100 120.0;;

let n = 135;;
let x = 0.64;;
let vat = (VAT n x);;
let uvat = unVAT n vat;;

uvat = x;;

printfn "%0.100f %0.100f" x uvat;;


(* 2.12 *)
let min f = 
    let mutable n = 0
    while f n <> 0 do
        n <- n + 1
    n;;

min (fun x -> x - 3);;

(* 2.13 *)
// curry : ('a * 'b -> 'c) -> 'a -> 'b -> 'c
let curry f a b = f(a, b);;

// Curry is a function which takes a function and 2 values and calls the function with those two values as a pair

// uncurry : ('a -> 'b -> 'c) -> 'a * 'b -> 'c
let uncurry a b c = fun (a:'a, b:'b) -> c;;

// Is this cheating???