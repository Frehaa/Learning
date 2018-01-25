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



(* 4.1 *)
(* 4.2 *)
(* 4.3 *)
(* 4.4 *)
(* 4.5 *)
(* 4.6 *)
(* 4.7 *)
(* 4.8 *)
(* 4.9 *)
