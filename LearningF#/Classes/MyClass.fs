module ClassModule2

[<Class>]
type MyClass()= 
    inherit ClassModule.MyAbstract()
    override this.Say () = printfn "Hi!"