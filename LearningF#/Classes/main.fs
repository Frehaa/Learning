open ClassModule2

// type MyClass() = 
//     inherit MyAbstract()
//     override this.Say () = printfn "Hello!"


[<EntryPoint>]
let main args = 
    let myClass = new MyClass()
    myClass.Say()
    printfn "%s" "Test"
    0