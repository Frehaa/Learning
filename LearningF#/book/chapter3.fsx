(* 3.1 *)

type Meridian = AM | PM ;;
let beforeTuple (hours1, minutes1, f1) (hours2, minutes2, f2) = 
    if f1 < f2 then true
    else if f2 < f1 then false
    else if hours1 < hours2 then true
    else if hours2 < hours1 then false
    else minutes1 < minutes2;;

beforeTuple (11,59,AM) (1,15,PM);;
beforeTuple (11,59,AM) (1,15,AM);;
beforeTuple (1,14,AM) (1,15,AM);;

type TimeOfDay = {meridian : Meridian; hours : int; minutes: int };;

let beforeRecord t1 t2 = t1 < t2;;

let t1 = {meridian = AM; hours = 11; minutes = 59};;
let t2 = {meridian = PM; hours = 1; minutes = 15};;

let t3 = {meridian = AM; hours = 1; minutes = 15};;

let t4 = {meridian = AM; hours = 1; minutes = 14};;

beforeRecord t1 t2;;
beforeRecord t1 t3;;
beforeRecord t4 t3;;

(* 3.2 *)
(* The former British currency had 12 pence to a shilling and 20 shillings to a pound. Declare
functions to add and subtract two amounts, represented by triples (pounds,shillings,pence) of
integers, and declare the functions when a representation by records is used. Declare the func-
tions in infix notation with proper precedences, and use patterns to obtain readable declaration *)
let c1 = (10, 19, 11);;
let c2 = (0, 0, 0);;

let ( +. ) (pounds1, shillings1, pence1) (pounds2, shillings2, pence2) = 
    let extraS = if (pence1 + pence2) > 11 then 1 else 0
    let extraP = if (shillings1 + shillings2 + extraS) > 19 then 1 else 0
    ((pounds1 + pounds2 + extraP), ((shillings1 + shillings2 + extraS) % 20), ((pence1 + pence2) % 12));;
    
c1 +. c2;;
c1 +. (0, 0, 1);;

type BritishCurrency = {pounds: int; shillings: int; pence: int}

let bc1 : BritishCurrency = {pounds = 10; shillings = 19; pence = 11};
let bc2 : BritishCurrency = {pounds = 0; shillings = 0; pence = 0};

let ( +% ) : BritishCurrency -> BritishCurrency -> BritishCurrency  = fun a b ->
    let carryShilling = if a.pence + b.pence >= 12 then 1 else 0
    let carryPound = if a.shillings + b.shillings + carryShilling >= 20 then 1 else 0
    {
        pounds = a.pounds + b.pounds + carryPound;
        shillings = (a.shillings + b.shillings + carryShilling) % 20; 
        pence = (a.pence + b.pence) % 12
    };;

bc1 +% bc2;;
bc1 +% {pounds = 0; shillings = 0; pence = 1};;


2 + 2;;
(* 3.3 *)
(* The set of complex numbers is the set of pairs of real numbers. Complex numbers behave almost
like real numbers if addition and multiplication are defined by:
(a,b) + (c,d) = (a + c,b + d)
(a,b) · (c,d) = (ac − bd,bc + ad)
1. Declare suitable infix functions for addition and multiplication of complex numbers.
2. The inverse of (a,b) with regard to addition, that is, −(a,b) , is (−a,−b) , and the inverse of
(a,b) with regard to multiplication, that is, 1/(a,b) , is (a/(a 2 +b 2 ),−b/(a 2 +b 2 )) (provided
that a and b arenotbothzero).Declareinfixfunctionsforsubtraction anddivisionofcomplex
numbers.
3. Use let-expressions in the declaration of the division of complex numbers in order to avoid
repeated evaluation of identical subexpressions *)

type Complex = float * float;;

let ( +.+ ) : Complex -> Complex -> Complex = fun (a,b) (c,d) -> (a + c, b + d);;
let ( *.* ) : Complex -> Complex -> Complex = fun (a,b) (c,d) -> (a * c - b * d, b * c + a * d);;
let (~-.) : Complex -> Complex = fun (a,b) -> (-a, -b);;


let complex1 = (1.5, 2.3);;
let complex2 = (3.3, 1.2);;

complex1 +.+ complex2;;
complex2 +.+ complex1;;

complex1 *.* complex2;;
complex2 *.* complex1;;

-. complex1;;

(* 3.4 *)
type StraightLine = float * float;;

let mirror : StraightLine -> StraightLine = fun (a, b) -> (a, -b);;

let LineToString : StraightLine -> string = 
    fun (a, b) -> 
    "y = " + string a + "x"+ (if b = 0.0 then "" else " + " + string b);;
    
let l : StraightLine = (5.0, 0.5);;

LineToString l;;

(* 3.5 *)
type Solution = int;;

let solve = 0;;

(* 3.6 *)
(* See 3.1 *)

(* 3.7 *)

type Shape = 
| Circle of float 
| Square of float 
| Triangle of float * float * float;;

let isShape = function
| Circle r -> r > 0.0
| Square a -> a > 0.0
| Triangle(a,b,c) ->
    a > 0.0 && b > 0.0 && c > 0.0
    && a < b + c && b < c + a && c < a + b;;

let area = function 
| x when not (isShape(x)) -> failwith "Error"
| x -> 1;;  