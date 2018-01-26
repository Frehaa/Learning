let myList = 3::2::[];;

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
(* 4.2 *)
(* 4.3 *)
(* 4.4 *)
(* 4.5 *)
(* 4.6 *)
(* 4.7 *)
(* 4.8 *)
(* 4.9 *)
