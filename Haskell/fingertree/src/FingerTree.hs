module FingerTree(FingerTree, create, pushLeft, pushRight, peakLeft, peakRight) where

data FingerTree a = Empty
                  | Single a
                  | Deep (Digit a) (FingerTree (Node a)) (Digit a)
                  deriving (Show)

data Node a = Node2 a a | Node3 a a a deriving (Show)
type Digit a = [a]

class Reduce f where 
    reducer :: (a -> b -> b) -> (f a -> b -> b)
    reducel :: (b -> a -> b) -> (b -> f a -> b)

-- instance Reduce Node where
--     reducer f (Node2 a b)   z = a `f` (b `f` z)
--     reducer f (Node3 a b c) z = a `f` (b `f` (c `f` z))
--     reducel f z (Node2 a b)   = (z `f` b) `f` a
--     reducel f z (Node3 a b c) = ((z `f` c) `f` b) `f` a

-- instance Reduce FingerTree where 
--     reducer f Empty           z = z
--     reducer f (Single x)      z = x `f` z
--     reducer f (Deep pr m sf)  z = pr `f'` (m `f''` (sf `f'` z))
--         where f'  = reducer f
--               f'' = reducer (reducer f)
--     reducel f z Empty          = z
--     reducel f z (Single x)     = z `f` x
--     reducel f z (Deep pr m sf) = pr `f'` (m `f''` (sf `f'` z))
--         where f'  = reducer 
--               f'' = reducer (reducer f)

create :: FingerTree a
create = Empty

pushLeft  :: a -> FingerTree a -> FingerTree a
pushLeft a (Empty) = Single a
pushLeft a (Single b) = Deep [a] Empty [b]
pushLeft a (Deep [b,c,d,e] m sf) = Deep [a, b] (pushLeft (Node3 c d e) m) sf
pushLeft a (Deep pr m sf) = Deep (a : pr) m sf

pushRight :: FingerTree a -> a -> FingerTree a
pushRight (Empty) a = Single a
pushRight (Single b) a = Deep [b] Empty [a]
pushRight (Deep pr m [e,d,c,b]) a = Deep pr (pushRight m (Node3 e d c)) [b, a]
pushRight (Deep pr m sf) a = Deep pr m (sf ++ [a])

peakRight :: FingerTree a -> Maybe a
peakRight Empty = Nothing
peakRight (Single x) = Just x
peakRight (Deep (x:_) _ _) = Just x

peakLeft :: FingerTree a -> Maybe a
peakLeft Empty = Nothing
peakLeft (Single x) = Just x
peakLeft (Deep _ _ (x:_)) = Just x

