let myList = 3::2::[];;
open System.Data.Odbc
open System.Runtime.InteropServices

for i in [0..10] do printfn "%i" i;;
for i in [0..10..100] do printfn "%i" i;;

for f in [0.5..12.5] do printfn "%0.1f" f;;
for f in [0.5..0.25..5.5] do printfn "%0.2f" f;;


// It is left as an exercise to give a declaration for altsum containing only two clauses. ???
let rec altsum = function 
| [] -> 0
| [x1] -> x1
| x1::x2::xs -> x1 - x2 + altsum xs;;

altsum [1..10];;

// 1 - 2 + 3 - 4 + 5 - 6 + 7 - 8 + 9 - 10
// 1 + 3 + 5 + 7 + 9 = 25
// -2 - 4 - 6 - 8 - 10 = -30

let add a b = (b, a);;

let add3 = add 3;;

add3 10;;

type ArticleCode = string;;
type ArticleName = string;;

type Price = int;;
type Register = (ArticleCode * (ArticleName*Price)) list;;

let reg : Register = [("a1", ("cheese", 25));
                      ("a2", ("herring", 4));
                      ("a3", ("soft drink", 5));]

type NoPieces = int;;
type Item = NoPieces * ArticleCode;;
type Purchase = Item list;;

let pur : Purchase = [(3, "a2"); (1, "a1")]

type Info = NoPieces * ArticleName * Price;;
type Infoseq = Info list;;
type Bill = Infoseq * Price;;

// : ArticleCode -> Register -> ArticleName * Price 
let rec findArticle ac = function 
| (ac', adesc)::_ when ac = ac' -> adesc
| _::reg -> findArticle ac reg
| _ -> failwith (ac + " is an unknown article code");;

// Register -> Purchase -> Bill
let rec makeBill reg = function
| [] -> ([], 0)
| (np, ac)::pur -> let (aname, aprice) = findArticle ac reg
                   let tprice = np*aprice
                   let (billtl, sumtl) = makeBill reg pur
                   ((np, aname, tprice)::billtl, tprice+sumtl) ;;

makeBill reg pur;;

(* Map Coloring *)
type Country = string;;
type Map = (Country * Country) list;;

let exMap = [("a", "b"); ("c", "d"); ("d", "a");]

type Color = Country list;; // All contries with this color
type Coloring = Color list;; // Disjoint colors

let rec isMember a = function
| [] -> false
| x::xs -> x = a || isMember a xs;;

let areNb m c1 c2 = isMember (c1, c2) m || isMember (c2, c1) m;;

let rec canBeExtBy m col c = 
    match col with
    | [] -> true
    | c'::col' -> not(areNb m c' c) && canBeExtBy m col' c;;

canBeExtBy exMap ["c"] "a";;

canBeExtBy exMap ["a"; "c"] "b";;

let rec extColoring m cols c = 
    match cols with
    | [] -> [[c]]
    | col::cols' -> if canBeExtBy m col c
                    then (c::col)::cols'
                    else col::extColoring m cols' c;;

extColoring exMap [] "a";;                    

extColoring exMap [["c"]] "a";;     

extColoring exMap [["b"]] "a";;     

extColoring exMap [["d"]] "a";;     

let addElem x ys = if isMember x ys then ys else x::ys;;

let rec countries = function
| [] -> []
| (c1, c2)::m -> addElem c1 (addElem c2 (countries m));;

let rec colCntrs m = function 
    | []    -> []
    | c::cs  -> extColoring m (colCntrs m cs) c;;

let colMap m = colCntrs m (countries m);;

colMap exMap;;

(* 4.1 *)
let upto n = [1..n];;
upto 10;;

(* 4.2 *)
let downto1 n = [n..(-1)..1];;
downto1 10;;

(* 4.3 *)
let evenN n = [2..2..n*2];;
evenN 5;;

(* 4.4 *)
let rec altsum2 = function
| [] -> 0
| x::xs -> x + -(altsum2 xs);;

altsum2 [1..3];;

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

(* 4.5 *)
let rec rmodd = function
| x::xs when x % 2 = 1 -> rmodd(xs)
| x::xs -> x::rmodd(xs)
| [] -> [];;

rmodd [1..1..100];;


(* 4.6 *)
let rec rmeven = function
| x::xs when x % 2 = 0 -> rmeven(xs)
| x::xs -> x::rmeven(xs)
| [] -> [];;

rmeven [1..100];;

(* 4.7 *)
let rec multiplicity x xs = 
    match xs with
    | [] -> 0    
    | x'::xs' -> 
        if x=x' then
            1 + multiplicity x xs'
        else
            multiplicity x xs';;

multiplicity 5 [1;5;2;5;3;5;4;5;5;];;
multiplicity 10 [];;

(* 4.8 *)
let rec split = function
| [x0;x1] -> ([x0],[x1])
| x0::x1::xs -> 
            let (odd, even) = split xs
            (x0::odd, x1::even)
| _         -> failwith "Invalid list size";;

split [1..10];;
split [1..11];;


(* 4.9 *)
let rec zip = function
| ([x0], [x1]) -> [(x0, x1)]
| (x0::xs0, x1::xs1) -> (x0, x1)::zip(xs0, xs1)
| _ -> failwith "Invalid list size";;

zip (split [1..10]);;
zip ([1..5], [11..15]);;
zip ([1;2], [11]);;

(* 4.10 *)
let rec prefix xs ys = 
    match xs, ys with
    | x::xs', y::ys' -> x = y && prefix xs' ys'
    | _::_, [] -> false
    | _ -> true;;
    // | [], _::_' -> true
    // | [], [] -> true;;

prefix [1..3] [1..5];;
prefix [2..5] [2..5];;
prefix [3..5] [2..5];;
prefix [3..5] [2..4];;
prefix [10..20] [10..14];;
prefix [1] [];;
prefix [] [1];;
prefix [1] [1];;

(* 4.11 *)

(* 1) *)
let rec count (xs, x) = 
    match xs with 
    | x'::_ when x' > x -> 0
    | x'::xs' when x' = x -> 1 + count (xs', x)
    | [] -> 0
    | _::xs' -> count(xs', x);;

    // if List.isEmpty xs then 0
    // else
    //     let x'::xs' = xs
    //     if x' > x then 0 
    //     else if x' = x then 1 + count (xs', x) 
    //     else count (xs', x);;

count ([1; 2; 3; 3; 3; 4; 3], 3);;
count ([3], 3);;
count ([], 3);;

(* 2) *)
let rec insert (xs, x) = 
    match xs with
    | (x'::_) as xs' when x' >= x -> x::xs'
    | x'::xs' when x' < x -> x'::insert(xs', x)
    | _ -> [x];;

insert ([1;3;6;10;], 5);;
insert ([], 5);;
insert ([1;3;6;10;], 15);;
insert ([5;6;7], 3)


(* 3) *)

let rec intersect (xs, ys) = 
    match xs, ys with
    | x::xs', y::ys' when x = y -> x::intersect (xs', ys')
    | x::xs', ((y::_) as ys') when x < y -> intersect (xs', ys')
    | (_::_) as xs', _::ys' -> intersect (xs', ys')
    | _, _ -> [];;

intersect([1;1;1;2;2], [1;1;2;4]);;
intersect([1;1;1;3;3;5;6], [2;2;4;7]);;
intersect([1], [1;1;2]);;
intersect([1], []);;

(* 4) *)
let rec plus (xs, ys) = 
    match xs, ys with
    | x::xs', ((y::_) as ys') when x < y -> x::plus(xs', ys')
    | _::_ as xs', y::ys' -> y::plus(xs', ys')
    | [] as xs', y::ys' -> y::plus(xs', ys')
    | x::xs', ([] as ys') -> x::plus(xs', ys')
    | _ -> [];;

plus([1;1;2],[1;2;4]);;
plus([],[1;2;4]);;
plus([1;1;2],[]);;
    
(* 5) *)
let rec minus (xs, ys) = 
    match xs, ys with
    | x::xs', y::ys' when x = y -> minus(xs', ys')
    | x::xs', (y::_ as ys') when x < y -> x::minus(xs', ys')
    | x::xs', [] -> x::minus(xs', [])
    | x::_ as xs', y::ys' when y < x -> minus(xs', ys')
    | [], _ -> []
    | _ -> [];;

minus([1;1;1;2;2],[1;1;2;3]);;
minus([1;1;2;3],[1;1;1;2;2]);;
minus([],[1;1;1;2;2]);;
minus([1;1;1;2;2], []);;

(* 4.12 *)
let rec sum p xs = 
    match xs with
    | x::xs' when p x -> x + sum p xs'
    | _::xs' -> sum p xs'
    | _ -> 0;;

sum (fun i -> i > 0)  [-10; 5; 3; -2; -5; 2];;
sum (fun i -> i > 0)  [];;
sum (fun i -> i > 0)  [-15];;
sum (fun i -> i > 0)  [15];;

(* 4.13 *)
(* 1) *)
let rec findMin xs = 
    match xs with
    | [x'] -> x'
    | x'::xs' -> min x' (findMin xs')
    | [] -> failwith "Invalid size";;

findMin [5;7;3;8;1];;
findMin [1;2;3;4;5;6;];;
findMin [5];;

(* 2) *)
let rec delete (a, xs) = 
    match xs with
    | x::xs' when x = a -> xs'
    | x::xs' -> x::delete(a, xs')
    | [] -> [];;

delete (5, [1;2;3]);;
delete (2, [1;2;3]);;
delete (2, []);;
delete (2, [2;2]);;

(* 3) *)
let rec sort xs = 
    match xs with
    | [] -> []
    | _ -> 
        let minimum = findMin xs
        let xs' = delete (minimum, xs)
        minimum::(sort xs');;

sort [1;2;3];;
sort [3;2;1;];;
sort [2;1;3;];;
sort [];;
sort [2];;

(* 4.14 *)
// Declare a function of type int list -> int option for finding the smallest element in an integer list.
let rec findMinOpt = function
| [] -> None
| [x] -> Some x
| x::xs -> min (Some x) (findMinOpt xs)

findMinOpt [5;7;3;8;1];;
findMinOpt [1;2;3;4;5;6;];;
findMinOpt [5];;
findMinOpt ([] : int list);;

(* 4.15 *)
(*  Declare an F# function revrev working on a list of lists, that maps a list to the reversed list of
the reversed elements, for example:
revrev [[1;2];[3;4;5]] = [[5;4;3];[2;1]]
*)

let rec rev = function 
| [] -> []
| x::xs -> (rev xs)@[x];;

let rec revrev = function 
    | [] -> []
    | x::xs -> (revrev xs)@[rev x];;
    
revrev [[1;2];[3;4;5]];;

(* 4.16 *)
let rec f = function
| (x, []) -> []
| (x, y::ys) -> (x+y)::f(x-1, ys);;

(10 ,[for _ in [1..10] do yield 0]) |> f;;

let rec g = function
| [] -> []
| (x,y)::s -> (x,y)::(y,x)::g s;;

let rec h = function
| [] -> []
| x::xs -> x::(h xs)@[x];;

[1;2;3;] |> h;;

(* 4.17 *)
(* 4.18 *)
(* 4.19 *)
(* 4.20 *)
(* 4.21 *)
(* 4.22 *)
(* 4.23 *)
