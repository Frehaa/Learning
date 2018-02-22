module Complex

type Complex = 
    | C of float * float
    override c.ToString() = 
        match c with 
        | C(a, b) -> "(" + string a + ", " + string b + ")"

let mkComplex (a, b) = C(a, b)

let multInverse c = 
    match c with 
    | C(a, b) when a = 0. && b = 0. ->  failwith "a and b are 0"
    | C(a, b) -> C(a/(a * a + b * b), -b/(a * a + b * b))

type Complex with
    static member (+) (C(a, b), C(c, d)) = C(a + c, b + d)
    static member (*)(C(a, b), C(c, d)) = C(a * c - b * d, b * c + a * d)
    static member (~-) (C(a, b)) = C(-a, -b)

    static member (-) (c1, c2 : Complex) = c1 + -c2
    static member (/) (c1, c2 : Complex) = c1 * (multInverse c2)