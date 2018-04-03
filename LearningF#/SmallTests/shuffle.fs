open System

/// Swaps two elements in an array
let swap a i j = 
    let tmp = Array.item i a
    a.[i] <- a.[j]
    a.[j] <- tmp

/// Takes a sequence and returns an array with the elements shuffled, using the Fisher-Yates method
let fisher a = 
    let a = Seq.toArray a
    let n = a.Length
    let r = Random()
    for i in [(n - 1) .. -1 .. 1] do
        let j = r.Next (i + 1)
        swap a i j
    a

let a = [|1;2;3;4;5|]

fisher a
a
