// 5.1 Give a declaration for List.filter using List.foldBack.

List.filter (fun x -> x < 5) [1..10];;
open System.Drawing

let myFilter f a= List.foldBack (fun x xs -> if f x then x::xs else xs) a [];;

myFilter (fun x -> x < 5) [1..10];;

// 5.2 Solve Exercise 4.15 using List.fold or List.foldBack.
(* 4.15 *)
(*  Declare an F# function revrev working on a list of lists, that maps a list to the reversed list of
the reversed elements, for example:
revrev [[1;2];[3;4;5]] = [[5;4;3];[2;1]]
*)

let rec revrev xs = 
    let rev xs = List.fold (fun xs x -> x::xs) [] xs
    List.fold (fun xs x -> (rev x)::xs) [] xs;;
revrev [[1;2];[3;4;5]];;

// 5.3 Solve Exercise 4.12 using List.fold or List.foldBack.
(* Declare a function sum (p,xs) where p is a predicate of type int -> bool and xs is a list of
integers. The value of sum (p,xs) is the sum of the elements in xs satisfying the predicate p .
Test the function on different predicates (e.g., p(x) = x > 0 ). *)

let sum (p, xs) = List.foldBack (fun x xs -> if p x then x::xs else xs) xs [];;
sum ((fun x -> x > 0), [-5..5])

(* 5.4 : Declare a function downto1 such that: 
    downto1 f n e = f(1,f(2,...,f(n−1,f(n,e))...)) for n > 0
    downto1 f n e = e for n ≤ 0 *)

let rec downto1 f n e = 
    match n with
    | x when x > 0 -> downto1 f (x - 1) (f(x, e))
    | _ -> e;;

// let rec downto1 f n e = 
//     if n <= 0 then e 
//     else List.fold (fun e n -> f (n, e)) e [n..(-1)..1];;


downto1 (fun (a, b) -> a + b) 5 0;;

(* Declare the factorial function by use of downto1. *)

let fact n = downto1 (fun (a, b) -> a * b) n 1;;

fact 5;; // 120

(* Use downto1 to declare a function that builds the list [ g(1),g(2),...,g(n) ] for a function g and an integer n . *)

let buildList g n = downto1 (fun (n, ns) -> g(n)::ns) n [];;

buildList id 10;; // [1; 2; 3; 4; 5; 6; 7; 8; 9; 10]


(* 5.5 
    Consider the map colouring example in Section 4.6. Give declarations for the functions areNb
    canBeExtBy, extColouring, countries and colCntrs using higher-order list functions. ??????????
    Are there cases where the old declaration from Section 4.6 is preferable? *) 

type Country = string;;
type Map = (Country * Country) list;;

// let areNb map c1 c2 = 

// areNb: Map -> Country -> Country -> bool
// Decides whether two countries are neighbours
// canBeExtBy: Map -> Colour -> Country -> bool
// Decides whether a colour can be extended by a country
// extColouring: Map -> Colouring -> Country -> Colouring
// Extends a colouring by an extra country
// countries: Map -> Country list
// Computes a list of countries in a map
// colCntrs: Map -> Country list -> Colouring
// Builds a colouring for a list of countries


(* 5.6 
    We define a relation from a set A to a set B as a subset of A × B. A relation r' is said to be
smaller than r, if r' is a subset of r, that is, if r' ⊆ r. A relation r is called finite if it is a
finite subset of A × B. Assuming that the sets A and B are represented by F# types ’a and ’b
allowing comparison we can represent a finite relation r by a value of type set<’a * ’b>. *)

let mySet = set [(1, "a");(1, "b");(2, "a");(3, "b")];;

(* 1. The domain dom r of a relation r is the set of elements a in A where there exists an element
b in B such that (a,b) ∈ r . Write an F# declaration expressing the domain function. *)

// {"a", "b", "c"} x {1, 2} = {("a", 1), ("a", 2), 
//                             ("b", 1), ("b", 2),
//                             ("c", 1), ("c", 2)}

let dom r = Set.fold (fun (xs : Set<'a>) (a, _) -> xs.Add a) (set []) r;;

dom mySet;;


(* 2. The range rng r of a relation r is the set of elements b in B where there exists an element a
in A such that (a,b) ∈ r . Write an F# declaration expressing the range function. *)

let range r = Set.fold (fun (xs : Set<'a>) (_, b) -> xs.Add b) (set []) r;;

range mySet;;

(* 3. If r is a finite relation from A to B and a is an element of A , then the application of r to a,
apply r a , is the set of elements b in B such that (a,b) ∈ r . Write an F# declaration expressing
the apply function. *)

// let apply r a = Set.fold (fun (xs :Set<'a>) (a', b) -> if a = a' then xs.Add b else xs) (set []) r;;

let apply r a = Set.filter (fun (a', _) -> a = a') r |> Set.map (fun (_, b) -> b);;

apply mySet 1;;

(* 4. A relation r from a set A to the same set is said to be symmetric if (a1 ,a2 ) ∈ r implies
(a2 , a1 ) ∈ r for any elements a 1 and a 2 in A . The symmetric closure of a relation r is the
smallest symmetric relation containing r. Declare an F# function to compute the symmetric
closure. *)

let myAASet = set [(1,1); (1,3); (2, 3); (3, 2); (3, 3); (4, 4)]

let rec symmetric r = 
    if Set.isEmpty r then r
    else 
        let (a, b) = r.MinimumElement
        let s = symmetric (r.Remove (a, b))
        if a = b then s.Add (a, b)
        else s;;
          
symmetric myAASet;;

(* 5. The relation composition r ◦◦ s of a relation r from a set A to a set B and a relation s from
B to a set C is a relation from A to C . It is defined as the set of pairs (a,c) where there exist
an element b in B such that (a,b) ∈ r and (b,c) ∈ s . Declare an F# function to compute the
relational composition. *)
let set1 = set [(1, 'a');(1, 'b');(2, 'b');(2, 'c');(3, 'c');]
let set2 = set [('a', "The a string" );('b', "The b string");('c', "The c string");('d', "The d string");('a', "The last string");] 

let addSet s r = Set.fold (fun (xs : Set<'a>) x -> xs.Add x) s r;;

let composition s r = 
    let rec compositionAccumulator s r t =
        if Set.isEmpty s then t
        else 
            let (a:'a, b:'b) = Set.minElement s
            let all = Set.fold (fun xs x -> Set.add (a, x) xs) t (apply r b)
            compositionAccumulator (s.Remove (a, b)) r all
    compositionAccumulator s r (set []);;

let rec composition2 s r = 
    if Set.isEmpty s then set []
    else
        let (a:'a, b:'b) = Set.minElement s
        let t = composition2 (s.Remove (a, b)) r
        Set.fold (fun xs x -> Set.add (a, x) xs) t (apply r b);;

composition set1 set2;;

(* 6. A relation r from a set A to the same set A is said to be transitive if (a1, a2) ∈ r and
(a2, a3) ∈ r implies (a1, a3) ∈ r for any elements a1, a2 and a3 in A. The transitive closure
of a relation r is the smallest transitive relation containing r. If r contains n elements, then
the transitive closure can be computed as the union of the following n relations:
r ∪ (r ◦◦ r) ∪ (r ◦◦ r ◦◦ r) ∪ ··· ∪ (r ◦◦ r ◦◦ ··· ◦◦ r)
Declare an F# function to compute the transitive closure. *)

let trans = set [(1,2);(2,3);(3,4);(4,5);];;

let rec transitiveClosure r = 
    let c = (composition r r)
    let u = (Set.union r c)
    if r = u then u
    else transitiveClosure u;;
    
transitiveClosure trans;;

(* 5.7 Declare a function allSubsets such that allSubsets n k is the set of all subsets of
{1,2,...,n} containing exactly k elements. Hint: use ideas from Exercise 2.8. For example,
? n
k
?
is the number of subsets of {1,2,...,n} containing exactly k elements. *)

// 5.8 Give declarations for makeBill3 using map.fold rather than map.foldBack.

// 5.9 Declare a function to give a purchase map (see Version 3 on Page 118) on the basis of a list of
// items (from the Versions 1 and 2).

// 5.10 Extend the cash register example to take discounts for certain articles into account. For example,
// find a suitable representation of discounts and revise the function to make a bill accordingly.

// 5.11 Give a solution for Exercise 4.23 using the Set and Map libraries.

(* 5.8 Give declarations for makeBill3 using map.fold rather than map.foldBack. *)
(* 5.9 Declare a function to give a purchase map (see Version 3 on Page 118) on the basis of a list of
items (from the Versions 1 and 2). *)
(* 5.10 Extend the cash register example to take discounts for certain articles into account. For example,
find a suitable representation of discounts and revise the function to make a bill accordingly. *)
(* 5.11 Give a solution for Exercise 4.23 using the Set and Map libraries. *)
