module Option (Option(Some,None)) where

data Option a = Some a | None deriving (Show, Eq)

instance Functor Option where
    fmap f (Some x) = Some (f x)
    fmap f None = None

instance Applicative Option where
    pure = Some
    Some f <*> Some x = Some (f x)
    _ <*> _ = None

instance Monad Option where
    return = Some
    None >>= f = None
    Some x >>= f = f x
    fail _ = None