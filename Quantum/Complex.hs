-- module Complex where

data Complex = Complex Double Double

instance Show Complex where
    show (Complex r 0.0) = show r
    show (Complex 0.0 i) = show i ++ "i"
    show (Complex r i) = show r ++ " + " ++ show i ++ "i"

instance Num Complex where
    (+) (Complex a1 b1) (Complex a2 b2) = Complex (a1 + a2) (b1 + b2)
    (-) (Complex a1 b1) (Complex a2 b2) = Complex (a1 - a2) (b1 - b2)
    (*) (Complex a1 b1) (Complex a2 b2) = Complex (a1 * a2 - b1*b2) (a1 * b2 + a2 * b1)
    abs c = c
    signum c = c
    fromInteger i = Complex (fromInteger i) 0

i = Complex 0 1

main = 
    let x = Complex 3 (-2)
        y = Complex 1 2 in
    print (x * y)


