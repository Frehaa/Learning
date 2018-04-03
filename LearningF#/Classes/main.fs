// type MyClass(n) =
//     let mutable _N = n
    
//     member this.N with get() = _N and set(value) = _N <- value 
//     member this.Say () = printfn "Hello!"
    
// type MyClass() = 
//     inherit MyAbstract()
//     override this.Say () = printfn "Hello!"

open MyInterface
    
type MyClass<'a, 'b> (x : 'a, y : 'b) =
    let _y = y // Redundant. Just redefinition of y with different name.
    let mutable x = x // Required if we want to change x. Could also name _x
    do 
        printfn "Creating Class"

    member this.X with get() = x and set(value) = x <- value
    // new () = MyClass<int, int>(5, 10)

    interface IMyInterface with
        member this.DoStuff () = printfn "Does %A and %A" x _y

type MyOther (v) =
    let _v = v

    member this.V with get() = _v
    new() = MyOther("5")


[<EntryPoint>]
let main args = 
    let myClass1 = new MyClass<string, int>("nothing", 5) :> IMyInterface
    myClass1.DoStuff()

    let myClass2 = new MyClass<string, char list>("a lot", ['a';'b']) :> IMyInterface
    myClass2.DoStuff()

    let myClass3 = new MyClass<string, char>("test", 'b')
    printfn "%A" myClass3.X
    myClass3.X <- "new Value"
    printfn "%A" myClass3.X

    let other1 = new MyOther("hi")
    printfn "%s" other1.V

    let defaultOther = new MyOther()
    printfn "%s" defaultOther.V

    // let myClass = new MyClass("Wow")
    // printfn "%s" myClass.N
    
    // printfn "%s" myClass.N

    // printfn "%s" "Test"

    0