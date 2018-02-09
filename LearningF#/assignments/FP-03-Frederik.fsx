// 3.1 
type 'a BinTree = 
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

mapInOrder ((+) 1) t5;;

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
I ``b = a * 5`` s;; // [("a", 1); [("b", 5)]]

let ``b = 10; c = b + a`` = Seq(Ass("b", N(10)), Ass("c", Add(V("b"), V("a"))))

I ``b = 10; c = b + a`` s;; // [("a", 1);("b", 10);("c", 11)]

let ``if a < 5 then a = a - a else a = 5`` = ITE(Lt(V("a"), N(5)), Ass("a", Sub(V("a"), V("a"))), (Ass("a", N(5))));;

I ``if a < 5 then a = a - a else a = 5`` s // [("a", 0)]

let ``b = 1; While b != 10 then b = b + 1; a = a + b`` = 
    Seq(Ass("b", N(1)), While(Neg(Eq(V("b"), N(10))), Seq(Ass("b", Add(V("b"), N(1))), Ass("a", Add(V("b"), V("a"))))))

I ``b = 1; While b != 10 then b = b + 1; a = a + b`` s; // [("a", 55): ("b", 10)]

let ``b = a; skip`` = Seq(Ass("b", V("a")), Skip);;

I ``b = a; skip`` s;; // [("a", 1): ("b", 1)]


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
(* 6.2 Postfix form is a particular representation of arithmetic expressions where each operator is
preceded by its operand(s), for example:
(x + 7.0) has postfix form x 7.0 +
(x + 7.0) ∗ (x − 5.0) has postfix form x 7.0 + x 5.0 − ∗
Declare an F# function with type Fexpr -> string computing the textual, postfix form of
expression trees from Section 6.2. *)
type Fexpr = 
    | Const of float
    | X
    | Add of Fexpr * Fexpr
    | Sub of Fexpr * Fexpr
    | Mul of Fexpr * Fexpr
    | Div of Fexpr * Fexpr
    | Sin of Fexpr
    | Cos of Fexpr
    | Log of Fexpr
    | Exp of Fexpr;;

let rec FexprToString = function
    | Const(v)      -> string v + " "
    | X             -> "x "
    | Add(f1, f2)   -> FexprToString f1 + FexprToString f2 + "+ " 
    | Sub(f1, f2)   -> FexprToString f1 + FexprToString f2 + "- " 
    | Mul(f1, f2)   -> FexprToString f1 + FexprToString f2 + "* " 
    | Div(f1, f2)   -> FexprToString f1 + FexprToString f2 + "/ " 
    | Sin(f)        -> FexprToString f + "sin "
    | Cos(f)        -> FexprToString f + "cos "
    | Log(f)        -> FexprToString f + "log "
    | Exp(f)        -> FexprToString f + "exp "

let exp1 = Add(X, Const(7.));;
let exp2 = Mul(exp1, Sub(X, Const(5.)));;
let exp3 = Log(exp2);;

FexprToString exp1;; // x 7 + 
FexprToString exp2;; // x 7 + x 5 - *
FexprToString exp3;; // x 7 + x 5 - * log 

// 3.8
type Instruction = | ADD | SUB | MULT | DIV | SIN
                   | COS | LOG | EXP | PUSH of float;;

type Stack = float list;;

let rec intpInstr s i = 
    match s, i with
    | (_, PUSH v)       -> v::s
    | (a::b::s', ADD)   -> intpInstr s' (PUSH(a + b))
    | (a::b::s', SUB)   -> intpInstr s' (PUSH(a - b))
    | (a::b::s', MULT)  -> intpInstr s' (PUSH(a * b))
    | (a::b::s', DIV)   -> intpInstr s' (PUSH(a / b))
    | (a::s', SIN)      -> intpInstr s' (PUSH(sin a))
    | (a::s', COS)      -> intpInstr s' (PUSH(cos a))
    | (a::s', LOG)      -> intpInstr s' (PUSH(log a))
    | (a::s', EXP)      -> intpInstr s' (PUSH(exp a))
    | _                 -> failwith "Not enough arguments on the stack";;

let a = intpInstr [] (PUSH 5.)  // [5.0]
let b = intpInstr a (PUSH 2.)   // [2.0; 5.0] 
let c = intpInstr b (ADD)       // [7.0]
let d = intpInstr c (PUSH 3.)   // [3.0; 7.0]
let e = intpInstr d (SUB)       // [-4.0]
let f = intpInstr e (PUSH -4.)  // [-4.0;-4.0]
let g = intpInstr f (MULT)      // [16.0]
let h = intpInstr g (LOG)       // [2.772588722]
let i = intpInstr h (EXP)       // [16.0]

let cosV = intpInstr [0.] (COS) // [1.0]
let sinV = intpInstr [0.] (SIN) // [0.0]

let takeFirst = function
    | []    -> failwith "Empty list"
    | x::_  -> x;;
   
List.find 

// let intpProg is = let [x] = List.fold intpInstr [] is in x;;
let intpProg = List.fold intpInstr [] >> takeFirst;;

intpProg [PUSH 5.;PUSH 2.;ADD; PUSH 3.; SUB;PUSH -4.; MULT] // 16.0

// 3.9
// 1)
// type Department = D of string * Department list;;

// 2)
type Department = D of string * float * Department list;;

// 3)

let d1  = D("BL", 10.0, []);;
let d2  = D("BR", 15.0, []);;
let d3  = D("ML", 100.0, [d1; d2]);;
let d4  = D("RR", 145.0, []);;
let d   = D("T", 1000.0, [d3;d4]);;
let rec extract (D(n, i, ds)) = (n, i) :: List.collect extract ds;;

extract d;; // [("T", 1000.0); ("ML", 100.0); ("BL", 10.0); ("BR", 15.0); ("RR", 145.0)]

// 4)

let rec t sum (D(n, i, ds)) = (i + List.fold t sum ds);;
let total = t 0.0;;

total d;; // 1270.0
   
// 5)

let rec extractTotal (D(n, i, ds) as d)  = (n, total d) :: List.collect extractTotal ds;; 

extractTotal d;; // [("T", 1270.0); ("ML", 125.0); ("BL", 10.0); ("BR", 15.0); ("RR", 145.0)]

// 6)
let rec dupn (s : string) = function
    | n when n > 0 -> s + dupn s (n-1)
    | _ -> "";;

// let spacing d = List.fold (fun s _ -> s + "    ") "" [1..d];;
let spacing d = dupn "    " d;;


let rec f d sum (D(n, _, ds))= spacing d + n + "\n" + List.fold (f (d + 1)) sum ds ;;
let format = f 0 "";;

printf "%s" (format d);;
(* 
TT
    RR
    ML
        BR
        BL
*) 