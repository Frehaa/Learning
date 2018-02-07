
let addOne x = x + 1;;
let multiplyTwo x = x * 2;;

let subtractTwo x = x - 2;;
let divideFive x = x / 5;;

let trans = [addOne; multiplyTwo; subtractTwo; divideFive];;

let init = 5;;

let rec applyBest s e fs = 
    match fs with 
    | []    -> s
    | f::fs -> e (f s) (applyBest s e fs);;

applyBest init max trans;;

let rec hillClimb s trans = 
    let best = applyBest s max trans
    if s = best then s
    else hillClimb best trans;; 

hillClimb init [addOne];;


let rec getMax init = 
    let next = init + 1
    if init > next then
        init
    else 
        getMax next;;
    