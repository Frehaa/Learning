let isMember x = List.exists ((=) x);;

let disjoint xs ys = List.forall (fun y -> (not << (isMember y)) xs) ys;;

disjoint [1..2] [3..4];;

#time
let listMaxBack xs = List.foldBack (max) xs 0;;

let listMax = List.fold (max) 0;;

listMaxBack [1..100000000]

Map.foldBack (fun a b c -> c) 