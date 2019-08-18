data Node a = 
      Node2 a a
    | Node3 a a a deriving(Show)

data Digit a = 
      Zero
    | One a
    | Two a a
    | Three a a a
    | Four a a a a deriving(Show)

-- Pushes out the right-most element if full
addL :: a -> Digit a -> Digit a
addL a Zero           = One a
addL a (One b)        = Two a b
addL a (Two b c)      = Three a b c
addL a (Three b c d)  = Four a b c d
addL a (Four b c d _) = Four a b c d

-- Pushes out the left-most element if full
addR :: Digit a -> a -> Digit a
addR Zero a           = One a
addR (One b) a        = Two b a
addR (Two c b) a      = Three c b a
addR (Three d c b) a  = Four d c b a
addR (Four _ d c b) a = Four d c b a

left :: Digit a -> a
left (One a)        = a
left (Two a _)      = a
left (Three a _ _)  = a
left (Four a _ _ _) = a

right :: Digit a -> a
right (One a)        = a
right (Two _ b)      = b
right (Three _ _ c)  = c
right (Four _ _ _ d) = d

removeL :: Digit a -> Digit a
removeL Zero = Zero
removeL (One _) = Zero
removeL (Two _ b) = One b
removeL (Three _ b c) = Two b c
removeL (Four _ b c d) = Three b c d

removeR :: Digit a -> Digit a
removeR Zero = Zero
removeR (One _) = Zero
removeR (Two a _) = One a
removeR (Three a b _) = Two a b
removeR (Four a b c _) = Three a b c

data FingerTree a = 
      Empty
    | Single a
    | Deep (Digit a) (FingerTree (Node a)) (Digit a) deriving(Show)

class Reduce f where
    reducer :: (a -> b -> b) -> (f a -> b -> b)
    reducel :: (b -> a -> b) -> (b -> f a -> b)

instance Reduce [] where
    reducer _ []        z = z
    reducer (*>) (x:xs) z = x *> (xs *>> z) where (*>>) = reducer (*>)
    reducel _ z []        = z
    reducel (<*) z (x:xs) = (z <* x) <<* xs where (<<*) = reducel (<*)

instance Reduce Digit where
    reducer (*>) (One a) z        = a *> z
    reducer (*>) (Two a b) z      = a *> (b *> z)
    reducer (*>) (Three a b c) z  = a *> (b *> (c *> z))
    reducer (*>) (Four a b c d) z = a *> (b *> (c *> (d *> z)))
    reducel (<*) z (One a)        = z <* a
    reducel (<*) z (Two a b)      = (z <* a) <* b
    reducel (<*) z (Three a b c)  = ((z <* a) <* b) <* c
    reducel (<*) z (Four a b c d) = (((z <* a) <* b) <* c) <* d

instance Reduce Node where
    reducer (*>) (Node2 a b)   z = a *> (b *> z)
    reducer (*>) (Node3 a b c) z = a *> (b *> (c *> z))
    reducel (<*) z (Node2 a b)   = (z <* a) <* b
    reducel (<*) z (Node3 a b c) = ((z <* a) <* b) <* c

instance Reduce FingerTree  where
    reducer _ Empty           z = z
    reducer (*>) (Single a)   z = a *> z
    reducer (*>) (Deep l t r) z = l *>> (t *>>> (r *>> z))
        where (*>>)  = reducer (*>)
              (*>>>) = reducer (reducer (*>))
    reducel _ z Empty           = z
    reducel (<*) z (Single a)   = z <* a
    reducel (<*) z (Deep l t r) = ((z <<* l) <<<* t) <<* r
        where (<<*)  = reducel (<*)
              (<<<*) = reducel (reducel (<*))

toList :: (Reduce f) => f a -> [a]
toList s = reducer (:) s []

(<+) :: a -> FingerTree a -> FingerTree a
a <+ Empty                    = Single a
a <+ Single b                 = Deep (One a) Empty (One b)
a <+ Deep (Four b c d e) m sf = Deep (Two a b) ((Node3 c d e) <+ m) sf
a <+ Deep pr m sf             = Deep (addL a pr) m sf

(+>) :: FingerTree a -> a -> FingerTree a
Empty +> a                    = Single a
Single b +> a                 = Deep (One b) Empty (One a)
Deep dl m (Four b c d e) +> a = Deep dl (m +> (Node3 b c d)) (Two e a)
Deep dl m dr +> a             = Deep dl m (addR dr a)

toTree :: (Reduce f) => f a -> FingerTree a
toTree s = reducer (<+) s Empty

data ViewL s a = NilL | ConsL a (s a) deriving (Show)

viewL :: FingerTree a -> ViewL FingerTree a
viewL Empty = NilL
viewL (Single x) = ConsL x Empty
viewL (Deep dl m dr) = ConsL (left dl) (deepL (removeL dl) m dr)

deepL :: Digit a -> FingerTree (Node a) -> Digit a -> FingerTree a
deepL Zero m sf = 
    case viewL m of 
        NilL -> toTree sf
        ConsL a m' -> Deep (reducer addL a Zero) m' sf
deepL pr m sf = Deep pr m sf

isEmpty :: FingerTree a -> Bool
isEmpty x = 
    case viewL x of
        NilL -> True
        ConsL _ _ -> False
    
headL :: FingerTree a -> a
headL x = case viewL x of ConsL a _ -> a

tailL :: FingerTree a -> FingerTree a
tailL x = case viewL x of ConsL _ x' -> x'

data ViewR s a = NilR | ConsR a (s a) deriving (Show)

viewR :: FingerTree a -> ViewR FingerTree a
viewR Empty = NilR
viewR (Single x) = ConsR x Empty
viewR (Deep dl m dr) = ConsR (right dr) (deepR dl m (removeR dr))

deepR :: Digit a -> FingerTree (Node a) -> Digit a -> FingerTree a
deepR dl m Zero = 
    case viewR m of 
        NilR -> toTree dl
        ConsR a m' -> Deep dl m' (reducer addL a Zero)
deepR dl m dr = Deep dl m dr
    
headR :: FingerTree a -> a
headR x = case viewR x of ConsR a _ -> a

tailR :: FingerTree a -> FingerTree a
tailR x = case viewR x of ConsR _ x' -> x'

