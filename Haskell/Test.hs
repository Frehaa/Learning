
data Expression = Val Int | Add Expression Expression deriving (Show)

main = print (Add (Val 5) (Val 2))