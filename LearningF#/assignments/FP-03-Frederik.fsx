// 3.1 

type 'a BinTree = 
open System.Windows.Forms.VisualStyles.VisualStyleElement.Tab
    | Leaf
    | Node of 'a * 'a BinTree * 'a BinTree;;

let t1 = Node (56, Leaf, Leaf);;
let t2 = Node (25, t1, Leaf);;
let t3 = Node (78, Leaf,Leaf);;
let t4 = Node (562, Leaf,t3);;
let t5 = Node (43, t2, t4);;

let rec inOrder = function
    | Leaf -> []
    | Node (v, l, r) -> inOrder l @ [v] @ inOrder r;;

inOrder t5;; // [56; 25; 43; 562; 78]


// 3.2

let rec mapInOrder f = function
    | Leaf -> Leaf
    | Node (v, l,r) -> 
        let l' = mapInOrder f l
        Node (f v, l', mapInOrder f r);;

mapInOrder id t5;;

(* Can you give an example of why mapInOrder might give a result different from mapPostOrder, 
but the result tree returned in both cases is still the same. *)

// If the map function has side effects the result of running in in-order or pre-order can be different
// e.g. if printing the value of the the current node processed, the order the values are printed will be different
// Or if changing a mutable variable, the result might also differ depending on the traversed order 

// 3.3
let floatBinTree = Node(43.0,Node(25.0, Node(56.0,Leaf, Leaf), Leaf), Node(562.0, Leaf, Node(78.0, Leaf,Leaf)));;

let rec foldInOrder f s = function
    | Leaf           -> s
    | Node (v, l, r) -> 
        let sl = foldInOrder f s l
        let s' = f v sl
        foldInOrder f s' r;;

foldInOrder (fun n a -> a + n) 0.0 floatBinTree;; // 764.0

let foldInOrder2 f b t = List.fold f b (inOrder t);;

foldInOrder2 (fun n a -> a + n) 0.0 floatBinTree;; // 764.0


// 3.4
type aExp = 
    | N of int
    | V of string
    | Add of aExp * aExp
    | Mul of aExp * aExp
    | Sub of aExp * aExp;;

let rec A a s = 
    match a with 
    | N n           -> n
    | V x           -> Map.find x s
    | Add(a1, a2)   -> A a1 s + A a2 s
    | Mul(a1, a2)   -> A a1 s * A a2 s
    | Sub(a1, a2)   -> A a1 s - A a2 s;;

// let five = N 5;;
// let x = V "x"
// let m = Mul(five, x)

// let s = Map [("x", 10)]
// A m s;;

type bExp =
    | TT
    | FF
    | Eq of aExp * aExp
    | Lt of aExp * aExp
    | Neg of bExp
    | Con of bExp * bExp;;

let rec B b s = 
    match b with 
    | TT            -> true
    | FF            -> false
    | Eq(a1, a2)    -> A a1 s = A a2 s
    | Lt(a1, a2)    -> A a1 s < A a2 s
    | Neg(b)        -> not (B b s)
    | Con(b1, b2)   -> B b1 s && B b2 s;;
    
type stm = 
    | Ass of string * aExp
    | Skip
    | Seq of stm * stm
    | ITE of bExp * stm * stm
    | While of bExp * stm;;

let update x v s = Map.add x v s;;

let rec I stm s =
    match stm with
        | Ass(x, a)        -> update x (A a s) s
        | Skip             -> s
        | Seq(stm1, stm2)  -> let s' = I stm1 s in I stm2 s'
        | ITE(b,stm1,stm2) -> if B b s then I stm1 s else I stm2 s
        | While(b, stm)    -> if B b s then let s' = I stm s in I (While(b, stm)) s' else s;;

let s = Map [("a", 1)];;

let ``b = a * 5`` = Ass("b", Mul(V("a"), N(5)));; 
I ``b = a * 5`` s;; // b = a * 5;

let ``b = 10; c = b + a`` = Seq(Ass("b", N(10)), Ass("c", Add(V("b"), V("a"))))

I ``b = 10; c = b + a`` s;; // b = 10; c = b + a;

let ``if a < 5 then a = a - a else a = 5`` = ITE(Lt(V("a"), N(5)), Ass("a", Sub(V("a"), V("a"))), (Ass("a", N(5))));;

I ``if a < 5 then a = a - a else a = 5`` s

let ``b = 1; While b != 10 then b = b + 1; a = a + b`` = 
    Seq(Ass("b", N(1)), While(Neg(Eq(V("b"), N(10))), Seq(Ass("b", Add(V("b"), N(1))), Ass("a", Add(V("b"), V("a"))))))

I ``b = 1; While b != 10 then b = b + 1; a = a + b`` s;

let ``b = a; skip`` = Seq(Ass("b", V("a")), Skip);;

I ``b = a; skip`` s;;


// 3.5
type stm2 = 
    | Ass of string * aExp
    | Skip
    | Seq of stm2 * stm2
    | ITE of bExp * stm2 * stm2
    | While of bExp * stm2
    | IT of bExp * stm2
    | RU of bExp * stm2;;

let rec I2 stm s =
    match stm with
        | Ass(x, a)         -> update x (A a s) s
        | Skip              -> s
        | Seq(stm1, stm2)   -> let s' = I2 stm1 s in I2 stm2 s'
        | ITE(b,stm1,stm2)  -> if B b s then I2 stm1 s else I2 stm2 s
        | While(b, stm)     -> if B b s then let s' = I2 stm s in I2 (While(b, stm)) s' else s
        | IT(b, stm)        -> if B b s then I2 stm s else s
        | RU(b, stm)        -> if B b s then s else let s' = I2 stm s in I2 (RU(b, stm)) s';;


// 3.6

// I would simply add the type to aExp 
//  type aExp =
//      ...
//      inc of aExp

// And handle the new case in A
//  let rec A a s = 
//      match a with 
//      ...
//      | inc(a') -> 1 + A a' s


// 3.7



// 3.8
// 3.9