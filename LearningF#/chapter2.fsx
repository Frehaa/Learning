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
let mutable i = 0;;

let str = "testing";;

let ch = 't';;

for c in str do if c = ch then i <- i + 1;;

i;;


// for c in str do c.ToString();;


(* 2.5 *)

(* 2.6 *)

(* 2.7 *)
(* 2.8 *)
(* 2.9 *)
(* 2.10 *)
(* 2.11 *)
(* 2.12 *)
(* 2.13 *)
