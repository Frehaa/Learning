module ClassModule2
[<Class>]
type MyClass = 
    inherit ClassModule.MyAbstract
    override Say : unit -> unit
    new : unit -> MyClass