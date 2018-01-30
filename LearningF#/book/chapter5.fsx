(* 5.1 Give a declaration for List.filter using List.foldBack.*)

(* 5.2 Solve Exercise 4.15 using List.fold or List.foldBack. *)

(* 5.3 Solve Exercise 4.12 using List.fold or List.foldBack. *)

(* 5.4 : Declare a function downto1 such that: 
    downto1 f n e = f(1,f(2,...,f(n−1,f(n,e))...)) for n > 0
    downto1 f n e = e for n ≤ 0 *)

let rec downto1 f n e = 
    match n with
    | x when x > 0 -> downto1 f (x - 1) (f(x, e))
    | _ -> e;;

let rec downto1 f n e = 
    if n <= 0 then e 
    else List.fold (fun e n -> f (n, e)) e [n..(-1)..1];;


downto1 (fun (a, b) -> a + b) 5 0;;

(* Declare the factorial function by use of downto1. *)

let fact n = downto1 (fun (a, b) -> a * b) n 1;;

fact 5;; // 120

(* Use downto1 to declare a function that builds the list [ g(1),g(2),...,g(n) ] for a function g and an integer n . *)

let buildList g n = downto1 (fun (n, ns) -> g(n)::ns) n [];;

buildList id 10;; // [1; 2; 3; 4; 5; 6; 7; 8; 9; 10]


(* 5.5 
    Consider the map colouring example in Section 4.6. Give declarations for the functions areNb
    canBeExtBy, extColouring, countries and colCntrs using higher-order list functions. 
    Are there cases where the old declaration from Section 4.6 is preferable? *)

(* 5.6 
    We define a relation from a set A to a set B as a subset of A × B . A relation r ? is said to be
smaller than r , if r ? is a subset of r , that is, if r ? ⊆ r . A relation r is called finite if it is a
finite subset of A × B . Assuming that the sets A and B are represented by F# types ’a and ’b
allowing comparison we can represent a finite relation r by a value of type set<’a * ’b>. *)

(* 1. The domain dom r of a relation r is the set of elements a in A where there exists an element
b in B such that (a,b) ∈ r . Write an F# declaration expressing the domain function. *)

(* 2. The range rng r of a relation r is the set of elements b in B where there exists an element a
in A such that (a,b) ∈ r . Write an F# declaration expressing the range function. *)

(* 3. If r is a finite relation from A to B and a is an element of A , then the application of r to a ,
applyr a , is thesetofelements b in B suchthat (a,b) ∈ r . WriteanF#declaration expressing
the apply function. *)

(* 4. A relation r from a set A to the same set is said to be symmetric if (a 1 ,a 2 ) ∈ r implies
(a 2 ,a 1 ) ∈ r for any elements a 1 and a 2 in A . The symmetric closure of a relation r is the
smallest symmetric relation containing r . Declare an F# function to compute the symmetric
closure. *)

(* 5. The relation composition r ◦◦ s of a relation r from a set A to a set B and a relation s from
B to a set C is a relation from A to C . It is defined as the set of pairs (a,c) where there exist
an element b in B such that (a,b) ∈ r and (b,c) ∈ s . Declare an F# function to compute the
relational composition. *)

(* 6. A relation r from a set A to the same set A is said to be transitive if (a 1 ,a 2 ) ∈ r and
(a 2 ,a 3 ) ∈ r implies (a 1 ,a 3 ) ∈ r for any elements a 1 ,a 2 and a 3 in A . The transitive closure
of a relation r is the smallest transitive relation containing r . If r contains n elements, then
the transitive closure can be computed as the union of the following n relations:
r ∪ (r ◦◦ r) ∪ (r ◦◦ r ◦◦ r) ∪ ··· ∪ (r ◦◦ r ◦◦ ··· ◦◦ r)
Declare an F# function to compute the transitive closure. *)

(* 5.7 Declare a function allSubsets such that allSubsets n k is the set of all subsets of
{1,2,...,n} containing exactly k elements. Hint: use ideas from Exercise 2.8. For example,
? n
k
?
is the number of subsets of {1,2,...,n} containing exactly k elements.
(* 5.8 Give declarations for makeBill3 using map.fold rather than map.foldBack.
(* 5.9 Declare a function to give a purchase map (see Version 3 on Page 118) on the basis of a list of
items (from the Versions 1 and 2).
(* 5.10 Extend the cash register example to take discounts for certain articles into account. For example,
find a suitable representation of discounts and revise the function to make a bill accordingly.
(* 5.11 Give a solution for Exercise 4.23 using the Set and Map libraries.