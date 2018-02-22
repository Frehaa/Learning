module Complex

[<Sealed>]
type Complex = 
    static member (+) : Complex * Complex -> Complex
    static member (-) : Complex * Complex -> Complex
    static member (*) : Complex * Complex -> Complex 
    static member (/) : Complex * Complex -> Complex 
    static member (~-) : Complex -> Complex

val mkComplex : float * float -> Complex