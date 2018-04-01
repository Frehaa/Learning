
type MyClass(n) =
    let mutable _N = n
    
    member this.N with get() = _N and set(value) = _N <- value 
    member this.Say () = printfn "Hello!"

    

[<EntryPoint>]
let main args = 
    let myClass = new MyClass("Wow")
    printfn "%s" myClass.N
    
    printfn "%s" myClass.N
    0