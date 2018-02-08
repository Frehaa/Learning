(* Exercise 2.1 : 
    Write a function 
    downTo:int->int list 
    so that downTo n returns the n-element list [n; n-1;. . .; 1]. You must use if-then-else expressions to define the function.*)

let rec downTo n = 
        if n < 0 then failwith "Invalid argument. n can't be < 0"
        else [n..(-1)..1];;

(* Secondly define the function downTo2 having same semantics as downTo. This time you must use pattern matching. *)
let rec downTo2 = function
| 0 -> []
| n when n > 0-> n::downTo2 (n-1)
| _ -> failwith "Invalid argument. n can't be < 0";;

(* Exercise 2.2 : 
    Write a function
    removeOddIdx:int list->int list
    so that removeOddIdx xs removes the odd-indexed elements from the list xs:
        removeOddIdx [x0; x1; x2; x3; x4; ...] = [x0; x2; x4; ...]
        removeOddIdx [] = []
        removeOddIdx [x0] = [x0]
*)     

let rec removeOddIdx = function 
| [] -> []
| [x] -> [x]
| x::_::xs -> x::removeOddIdx xs;;


(* Exercise 2.3 :
    Write a function
    combinePair:int list->(int * int) list
    so that combinePair xs returns the list with elements from xs combined into pairs.
    If xs contains an odd number of elements, then the last element is thrown away:
        combinePair [x1; x2; x3; x4] = [(x1,x2);(x3,x4)]
        combinePair [x1; x2; x3] = [(x1,x2)]
        combinePair [] = []
        combinePair [x1] = []
        Hint: Try use pattern matching.
*)
let rec combinePair = function
| x0::x1::xs -> (x0,x1)::combinePair xs
| _ -> [];;

combinePair [1..4];;
combinePair [1..3];;
combinePair ([] : int list);;
combinePair [1];;

(* Exercise 2.4 : Solve HR, exercise 3.2. *)
(* The former British currency had 12 pence to a shilling and 20 shillings to a pound. Declare
functions to add and subtract two amounts, represented by triples (pounds,shillings,pence) of
integers, and declare the functions when a representation by records is used. Declare the func-
tions in infix notation with proper precedences, and use patterns to obtain readable declaration *)

let toPence (l, s, p) = l * 20 * 12 + s * 12 + p
let normalize s = (s/(20*12), (s%(20*12))/12, ((s%(20*12))%12))
    
let (.+.) l r = normalize ((toPence l) + (toPence r))
let (.-.) l r = normalize ((toPence l) - (toPence r))

toPence (5,3,2) // 1238
normalize 1238 // (5,3,2)

(1,2,3) .+. (1,2,3) // (2,4,6)
(1,19,11) .+. (0,0,1) // (2,0,0)
(0,0,320) .+. (0,0,0) // (1,6,8)

(6,0,0) .-. (5,3,2) // (0,16,10)
(5,3,2) .-. (6,0,0) // (0, -16, -10)

type BritishCurrency = { pounds : int; shillings : int; pence : int; }

let toPence (c : BritishCurrency) = c.pounds * 20 * 12 + c.shillings * 12 + c.pence;;
let normalize s = {pounds = s/(20*12); shillings = (s%(20*12))/12; pence = (s%(20*12))%12}

let (%+%) l r = normalize ((toPence l) + (toPence r))
let (%-%) l r = normalize ((toPence l) - (toPence r))

toPence {pounds=5; shillings=3; pence=2} // 1238
normalize 1238 // {pounds = 5; shillings = 3; pence = 2}

{pounds = 1; shillings = 2; pence = 3}   %+% {pounds = 1; shillings = 2; pence = 3} // {pounds = 2; shillings = 4; pence = 6}
{pounds = 1; shillings = 19; pence = 11} %+% {pounds = 0; shillings = 0; pence = 1} // {pounds = 2; shillings = 0; pence = 0}
{pounds = 0; shillings = 0; pence = 320} %+% {pounds = 0; shillings = 0; pence = 0} // {pounds = 1; shillings = 6; pence = 8}

{pounds = 6; shillings = 0; pence = 0} %-% {pounds = 5; shillings = 3; pence = 2} // {pounds = 0; shillings = 16; pence = 10}
{pounds = 5; shillings = 3; pence = 2} %-% {pounds = 6; shillings = 0; pence = 0} // {pounds = 0; shillings = -16; pence = -10}

(* Exercise 2.5 : Solve HR, exercise 3.3. *)
(* The set of complex numbers is the set of pairs of real numbers. Complex numbers behave almost
like real numbers if addition and multiplication are defined by:
(a,b) + (c,d) = (a + c,b + d)
(a,b) · (c,d) = (ac − bd,bc + ad)
1. Declare suitable infix functions for addition and multiplication of complex numbers.
2. The inverse of (a,b) with regard to addition, that is, −(a,b) , is (−a,−b) , and the inverse of
(a,b) with regard to multiplication, that is, 1/(a,b) , is (a/(a 2 +b 2 ),−b/(a 2 +b 2 )) (provided
that a and b are not both zero). Declare infix functions for subtraction and division of complex
numbers.
3. Use let-expressions in the declaration of the division of complex numbers in order to avoid
repeated evaluation of identical subexpressions *)

type Complex = float * float;;

let ( +.+ ) (a,b) (c,d) = (a + c, b + d) : float * float;;
let ( *.* ) (a,b) (c,d) = (a * c - b * d, b * c + a * d) : float * float;;
let ( ~-. ) (a, b) = (-a, -b) : float * float;;
let ( -.- ) c1 c2 = c1 +.+ -.c2 : float * float;;
let ( ~%% ) (a, b)= 
    let c = (a*a + b*b)
    (a/c, -b/c) : float * float;;
let ( /./ ) c1 c2 = c1 *.* %%c2  : float * float;;

let complex1 = (1.5, 2.3);;
let complex2 = (3.3, 1.2);;

complex1 +.+ complex2;;
complex2 +.+ complex1;;

complex1 *.* complex2;;
complex2 *.* complex1;;

-. complex1;;
-. complex2;;

complex1 -.- complex2;;
complex2 -.- complex1;;

%% complex1;;
%% complex2;;

complex1 /./ complex2;;
complex2 /./ complex1;;

(* Exercise 2.6 : Solve HR, exercise 4.4 *)
let rec altsum = function
| [] -> 0
| x::xs -> x + -(altsum xs);;

altsum [1..3];;

// altsum2 [1, 2, 3]
// -> 1 + -(altsum2 [2, 3])
// -> 1 + -(2 + -(altsum2 [3]))
// -> 1 + -(2 + -(3 + -(altsum2 [])))
// -> 1 + -(2 + -(3 + -(0)))
// -> 1 + -(2 + -(3))
// -> 1 + -(2 -3)
// -> 1 - 2 + 3
// -> -1 + 3
// -> 2

(* Exercise 2.7 : 
    Write a function
    explode:string->char list
    so that explode s returns the list of characters in s:
        explode "star" = [’s’;’t’;’a’;’r’]
    Hint: if s is a string then s.ToCharArray() returns an array of characters. You can then use List.ofArray to turn it into a list of characters. *)
let explode (s : string) = List.ofArray <| s.ToCharArray ();;

explode "star";;

(*  Now write a function 
    explode2:string->char list 
    similar to explode except that you now have to use the string function s.Chars (or .[]), 
    where s is a string. You can also make use of s.Remove(0,1). The definition of explode2 will be recursive. *)

let rec explode2 (s : string) =
        match s with
        | "" -> []
        | s -> s.[0]::(explode2 (s.Remove (0, 1)));;

explode2 "star";;

(* Exercise 2.8 :
    Write a function
    implode:char list->string
    so that implode s returns the characters concatenated into a string:
        implode [’a’;’b’;’c’] = "abc"
    Hint: Use List.foldBack. *)

let implode (xs : char list)  = List.foldBack (fun c s -> c.ToString() + s) xs "";;

implode <| explode "star";; // "star"

(* Now write a function
    implodeRev:char list->string
    so that implodeRev s returns the characters concatenated in reverse order into a string:
        implodeRev [’a’;’b’;’c’] = "cba"
    Hint: Use List.fold. *)
let implodeRev (xs : char list)  = List.fold (fun s c -> c.ToString() + s) "" xs;;

implodeRev <| explode "star";; // "rats"

(* Exercise 2.9 : 
    Write a function
    toUpper:string->string
    so that toUpper s returns the string s with all characters in upper case:
        toUpper "Hej" = "HEJ"
    Hint: Use System.Char.ToUpper to convert characters to upper case. You can do it in one line using implode, List.map and explode. *)

let toUpper s = implode (List.map (System.Char.ToUpper) (explode s));;

toUpper "Testing"

(* Write the same function
    toUpper1
    using forward function composition
    ((f >> g) x = g(f x)) *)

let toUpper1 = explode >> List.map System.Char.ToUpper >> implode;;

toUpper1 "testing";;

(* Write the same function
    toUpper2
    using the pipe-forward operator (|>) and backward function composition (<<).
    Hint: << is defined as (f << g) x = (f o g) x = f(g(x)).
    Hint: |> is defined as x |> f = f x.
    The two operators are by default supported by F#. You can have F# interactive print the types:
    > (<<);;
    val it : ((’a -> ’b) -> (’c -> ’a) -> ’c -> ’b) = <fun:it@3-4>
    > (|>);;
    val it : (’a -> (’a -> ’b) -> ’b) = <fun:it@4-5>
    > (>>);;
    val it : ((’a -> ’b) -> (’b -> ’c) -> ’a -> ’c) = <fun:it@5-6> *)

let toUpper2 = implode << (System.Char.ToUpper |> List.map) << explode;;

(* Exercise 2.10 :
    Write a function
    palindrome:string->bool,
    so that palindrome s returns true if the string s is a palindrome; otherwise false.
    A string is called a palindrome if it is identical to the reversed string, eg, “Anna” is a palindrome but “Ann” is not.
    The function is not case sensitive. *)
let palindrome s = (toUpper s) = (toUpper (implodeRev (explode s))); // lots of wasted work but easy

palindrome "A";; // true
palindrome "Ann";; // false
palindrome "Anna";; // true


(* Exercise 2.11 :
    The Ackermann function is a recursive function where both value and number of mutually recursive calls grow rapidly.
    Write the function
    ack:int * int->int
    that implements the Ackermann function using pattern matching on the cases of (m,n) as given below.
    Notice: The Ackermann function is defined for non negative numbers only. *)

let rec ack = function
| (0, n) -> n + 1
| (m, 0) -> ack (m - 1, 1)
| (m, n) when m > 0 && n > 0 -> ack(m - 1, ack (m, n - 1))
| _ -> failwith "Invalid argument";;

ack (3, 11);; // 16381

(* Exercise 2.12
    The function
    time f:(unit->’a)->’a * TimeSpan
    below times the computation of f x
    and returns the result and the real time used for the computation. *)
    
let time f =
    let start = System.DateTime.Now in
    let res = f () in
    let finish = System.DateTime.Now in
    (res, finish - start);;

(* Try compute time (fun () -> ack (3,11)). *)

time (fun () -> ack (3, 11));; // 00:00:00.2724184

(* Write a new function
    timeArg1 f a :  (’a -> ’b) -> ’a -> ’b * TimeSpan 
    that times the computation of evaluating the function f with argument a. Try timeArg1 ack (3,11).
    Hint: You can use the function time above if you hide f a in a lambda (function). *)

let timeArg1 f a = time (fun () -> f a);;

timeArg1 ack (3, 11);; // 00:00:00.3040214

(* Exercise 2.13 : HR exercise 5.4 *)
(* Declare a function downto1 such that: 
    downto1 f n e = f(1,f(2,...,f(n−1,f(n,e))...)) for n > 0
    downto1 f n e = e for n ≤ 0 *)

let rec downto1 f n e = 
    match n with
    | x when x > 0 -> downto1 f (x - 1) (f(x, e))
    | _ -> e;;

// Alternative version using fold
let rec downtofold f n e = 
    if n <= 0 then e 
    else List.fold (fun e n -> f (n, e)) e [n..(-1)..1];;

downto1 (fun (a, b) -> a + b) 5 0;;

(* Declare the factorial function by use of downto1. *)

let fact n = downto1 (fun (a, b) -> a * b) n 1;;

fact 5;; // 120

(* Use downto1 to declare a function that builds the list [ g(1),g(2),...,g(n) ] for a function g and an integer n . *)

let buildList g n = downto1 (fun (n, ns) -> g(n)::ns) n [];;

buildList id 10;; // [1; 2; 3; 4; 5; 6; 7; 8; 9; 10]