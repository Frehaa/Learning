let sum = List.fold (+) 0;;
let reverse = List.fold (fun xs x -> x::xs) ([] : int list);;

sum [1..100];;

let x = 
    let x = 5 in let y = 10 in x + y;;

let rec power x n =
    match x, n with
    | _, 0 -> 1.0
    | x, n -> x * power x (n-1)
  
power 2.0 2

// "A function is a closure" "Represents memory which has been created"

(* Exercises *)

