module ClassModule2

[<Class>]
type MyClass(message)= 
    inherit ClassModule.MyAbstract()
    override this.Say () = printfn "%s" message