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
    | [], _::_' -> true
    | [], [] -> true;;

prefix [1..3] [1..5];;
prefix [2..5] [2..5];;
prefix [3..5] [2..5];;
prefix [3..5] [2..4];;
prefix [10..20] [10..14];;

(* 4.11 *)
  

(* 4.12 *)
(* 4.13 *)


(* 4.14 *)
(* 4.15 *)


(* 4.16 *)
(* 4.17 *)


(* 4.18 *)
(* 4.19 *)


(* 4.20 *)
(* 4.21 *)


(* 4.22 *)
