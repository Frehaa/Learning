-- module Complex where

data Complex = Complex Double Double

instance Show Complex where
    show (Complex r 0.0) = show r
    show (Complex 0.0 1.0) = "i"
    show (Complex 0.0 i) = show i ++ "i"
    show (Complex r 1.0) = show r ++ " + " ++ "i"
    show (Complex r i) = show r ++ " + " ++ show i ++ "i"

instance Num Complex where
    (+) (Complex r1 i1) (Complex r2 i2) = Complex (r1 + r2) (i1 + i2)
    (-) (Complex r1 i1) (Complex r2 i2) = Complex (r1 - r2) (i1 - i2)
    (*) (Complex r1 i1) (Complex r2 i2) = Complex (r1 * r2 - i1*i2) (r1 * i2 + r2 * i1)
    abs c = c
    signum c = c
    fromInteger i = Complex (fromInteger i) 0

scale :: Double -> Complex -> Complex
scale s (Complex r i) = Complex (s * r) (s * i)


modulus :: Complex -> Double
modulus (Complex r i) = sqrt (r^2 + i^2)

div :: Complex -> Complex -> Complex
div (Complex r1 i1) (Complex r2 i2) = Complex ((r1*r2 + i1*i2) / m) ((r2*i1 - r1*i2) / m)
        where m = r2^2 + i2^2

i = Complex 0 1

main = 
    let x = Complex 3 (-2)
        y = Complex 1 2 in
    print (x * y)
