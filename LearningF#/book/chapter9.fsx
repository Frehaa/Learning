let rec bigList n = if n=0 then [] else 1::bigList(n-1);;

bigList 13000 // Works
bigList 15799 // Works
bigList 15800 // Stack Overflow

// Iterative stuff

let rec bigListA n xs = 
    if n=0 then xs
    else bigListA (n-1) (1::xs);;

bigListA 15800 []
bigListA (15800 * 100)[]
bigListA 100000000 [] // Slow but works???


// Continuations stuff
let rec bigListC n c = if n = 0 then c [] else bigListC (n-1) (fun res -> c(1::res))

let bigList n = bigListC n id
bigList 2

let c0 = id
let c1 res = c0(1::res)
let c2 res = c1(1::res)

c2 [] // [1;1]
// c1 res = id(1::res) = 1::res
// c2 res = c1(1::res) = id(1::(1::res))
// c3 res = c2(1::res) = id(1::(1::(1::res)))

let add1 xs = 1::xs
let test = [] |> add1 |> add1 |> add1