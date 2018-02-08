let a = Some 1;;

let b = Some 3;; 

a < b;;

//  SLIDES 19 (9:20): How the f is the other way not also just "doing the recursion first"?

List.fold (fun xs x -> x::xs) [] [1..10];;

List.foldBack (fun x xs -> x::xs) [1..10] [] ;;
