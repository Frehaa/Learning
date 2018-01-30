
let sum = List.fold (+) 0;;
let reverse = List.fold (fun xs x -> x::xs) ([] : int list);;

sum [1..100];;