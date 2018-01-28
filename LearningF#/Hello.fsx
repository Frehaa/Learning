// open System;;
// [<EntryPoint>]
// let main(param: string[]) = 
//     printf "Hello, World!"
//     0;;

open System

let rec (<=.) xs ys = 
  match (xs,ys) with
    ([],_) -> true
  | (_,[]) -> false
  | (x::xs',y::ys') -> x<=y && xs' <=. ys';;

let r2 = [1;2;3] <=. [1;2];;
let r3 = [1;2] <=. [1;2;3];;
let r4 = [System.Math.Sin;System.Math.Cos]


let k :float = 1.0/5.0;;

k*3.0;;
k*4.0;; 
k*5.0;;


sqrt(25.0) = sqrt(9.0) + sqrt(16.0);;


