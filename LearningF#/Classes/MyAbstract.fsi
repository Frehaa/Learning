module ClassModule

[<AbstractClass>]
type MyAbstract =
    new : unit -> MyAbstract
    abstract member Say : unit -> unit
    