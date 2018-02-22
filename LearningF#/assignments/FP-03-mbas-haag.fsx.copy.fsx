// Functional Programming: Assignment 3
// Mathias Bastholm <mbas@itu.dk>
// Frederik Haagesen <haag@itu.dk>

// Exercise 3.1
type 'a BinTree = 
    Leaf
  | Node of 'a * 'a BinTree * 'a BinTree

let intBinTree = 
    Node(43,
        Node(25,
            Node(56, Leaf, Leaf),
            Leaf),
        Node(562,
            Leaf,
            Node(78, Leaf, Leaf)))

let rec inOrder = function
                  | Leaf -> []
                  | Node(v, l, r) -> (inOrder l) @ [v] @ (inOrder r)

inOrder intBinTree // [56; 25; 43; 562; 78]

// Exercise 3.2
let rec mapInOrder f tree = match tree with
                            | Leaf -> Leaf
                            | Node(v, l, r) -> let l' = mapInOrder f l
                                               let v' = f v
                                               let r' = mapInOrder f r
                                               Node(v', l',  r')

mapInOrder (fun x -> x*2) intBinTree

// An example of diffrence between mapInOrder and mapPreOrder:
//   If there are side effects in the mapping, for example printing the value,
//   then the result tree is the same, but the side effects happen in a different order

// Exercise 3.3
let rec foldInOrder f n tree =  match tree with
                                | Leaf -> n
                                | Node(v, l, r) -> let n' = foldInOrder f n l
                                                   let n'' = f n' v
                                                   foldInOrder f n'' r

let rec foldInOrder2 f n tree = List.fold f n (inOrder tree)

let floatBinTree = 
    Node(43.,
        Node(25.,
            Node(56., Leaf, Leaf),
            Leaf),
        Node(562.,
            Leaf,
            Node(78., Leaf, Leaf)))

foldInOrder (+) 0. floatBinTree // 764.
foldInOrder2 (+) 0. floatBinTree // 764.

// Exercise 3.4
type aExp =                 // Arithmetical expressions
    | N of int              // Numbers
    | V of string           // Variables
    | Add of aExp * aExp    // Addition
    | Mul of aExp * aExp    // Multiplication
    | Sub of aExp * aExp    // Subtraction

let rec A a s = 
    match a with
    | N n -> n
    | V v -> Map.find v s
    | Add(x,y) -> (A x s) + (A y s)
    | Mul(x,y) -> (A x s) * (A y s)
    | Sub(x,y) -> (A x s) - (A y s)

type bExp =                 // Boolean expressions
    | TT                    // True
    | FF                    // False
    | Eq of aExp * aExp     // Equality
    | Lt of aExp * aExp     // Less than
    | Neg of bExp           // Negation
    | Con of bExp * bExp    // Conjunction (and)

let rec B b s = 
    match b with
    | TT -> true
    | FF -> false
    | Eq(x,y) -> A x s = A y s
    | Lt(x,y) -> A x s < A y s
    | Neg(x) -> not (B x s)
    | Con(x,y) -> (B x s) && (B y s)

type stm =                      // Statements
    | Ass of string * aExp      // Assignment
    | Skip
    | Seq of stm * stm          // Sequential composition
    | ITE of bExp * stm * stm   // If-then-else
    | While of bExp * stm       // While

let update key value state = Map.add key value state

let rec I stm s = 
    match stm with
    | Ass(x,a) -> update x (A a s) s
    | Skip -> s
    | ITE(b,trueStm,falseStm) -> if (B b s) then I trueStm s else I falseStm s
    | While(b,stm) -> if (B b s)
                      then I (While(b,stm)) (I stm s)
                      else s
    | Seq(stm1,stm2) -> I stm2 (I stm1 s)

let fac = Seq(Ass("y", N 1),
              While(Neg(Eq(V "x", N 0)),
                    Seq(Ass("y", Mul(V "x", V "y")),
                        Ass("x", Sub(V "x", N 1)) )))
I fac (Map.ofList [("x", 4)]) // map [("x", 0); ("y", 24)]

// Exercise 3.5
type stm2 =                       // Statements
    | Ass of string * aExp        // Assignment
    | Skip
    | Seq of stm2 * stm2          // Sequential composition
    | ITE of bExp * stm2 * stm2   // If-then-else
    | While of bExp * stm2        // While
    | IT of bExp * stm2           // If-then
    | Rep of stm2 * bExp          // Repeat-until

let rec I2 stm s = 
    match stm with
    | Ass(x,a) -> update x (A a s) s
    | Skip -> s
    | ITE(b,trueStm,falseStm) -> if (B b s) then I2 trueStm s else I2 falseStm s
    | While(b,stm) -> if (B b s)
                      then I2 (While(b,stm)) (I2 stm s)
                      else s
    | Seq(stm1,stm2) -> I2 stm2 (I2 stm1 s)
    | IT(b,stm) -> I2 (ITE(b,stm,Skip)) s
    | Rep(stm,b) -> I2 (While(Neg(b), stm)) (I2 stm s)

let fac2 = Seq(Ass("y", N 1),
               Rep(Seq(Ass("y", Mul(V "x", V "y")),
                       Ass("x", Sub(V "x", N 1)) ),
                   Eq(V "x", N 1)) )
I2 fac2 (Map.ofList [("x", 4)]) // map [("x", 1); ("y", 24)]

let trueFalse = IT(Lt(V "x", N 4), Ass("z", N 10))
I2 trueFalse (Map.ofList [("x", 4)]) // map [("x", 4)]
I2 trueFalse (Map.ofList [("x", 1)]) // map [("x", 4); ("z", 10)]
 
// Exercise 3.6
type stm3 =                       // Statements
    | Ass of string * aExp        // Assignment
    | Skip
    | Seq of stm3 * stm3          // Sequential composition
    | ITE of bExp * stm3 * stm3   // If-then-else
    | While of bExp * stm3        // While
    | IT of bExp * stm3           // If-then
    | Rep of stm3 * bExp          // Repeat-until
    | Inc of string               // Increment variable

let rec I3 stm s = 
    match stm with
    | Ass(x,a) -> update x (A a s) s
    | Skip -> s
    | ITE(b,trueStm,falseStm) -> if (B b s) then I3 trueStm s else I3 falseStm s
    | While(b,stm) -> if (B b s)
                      then I3 (While(b,stm)) (I3 stm s)
                      else s
    | Seq(stm1,stm2) -> I3 stm2 (I3 stm1 s)
    | IT(b,stm) -> I3 (ITE(b,stm,Skip)) s
    | Rep(stm,b) -> I3 (While(Neg(b), stm)) (I3 stm s)
    | Inc(v) -> I3 (Ass(v, Add(V v, N 1))) s

// Increments `x`, `n` times. S0 = x: 0, n: 10
I3 (Rep(Seq(Inc("x"), Ass("n", Sub(V "n", N 1))), Lt(V "n", N 1))) (Map.ofList [("x", 0); ("n", 10)]) // map [("n",0); ("x",10)]

// Exercise 3.7
// (HR 6.2)

type Fexpr = | Const of float
             | X
             | Add of Fexpr * Fexpr
             | Sub of Fexpr * Fexpr
             | Mul of Fexpr * Fexpr
             | Div of Fexpr * Fexpr
             | Sin of Fexpr
             | Cos of Fexpr
             | Log of Fexpr
             | Exp of Fexpr

// Postfix string
let rec PFS = 
    function
    | Const(f) -> string f
    | X -> "x"
    | Add(x,y) -> PFS x + " " + PFS y + " +"
    | Sub(x,y) -> PFS x + " " + PFS y + " -"
    | Mul(x,y) -> PFS x + " " + PFS y + " *"
    | Div(x,y) -> PFS x + " " + PFS y + " /"
    | Sin(x) -> PFS x + " sin"
    | Cos(x) -> PFS x + " cos"
    | Log(x) -> PFS x + " log"
    | Exp(x) -> PFS x + " exp"

PFS (Add(X, Const 7.)) // "x 7 +"
PFS (Mul(Add(X, Const 7.), Sub(X, Const 5.))) // "x 7 + x 5 - *"

// Exercise 3.8
// (HR 6.8)
type Instruction = | ADD | SUB | MULT | DIV | SIN
                   | COS | LOG | EXP | PUSH of float

// 1.
type Stack = float list

let intpInstr (s:Stack) i = 
    match s, i with
    | a::b::s, ADD -> (b + a)::s
    | a::b::s, SUB -> (b - a)::s
    | a::b::s, MULT -> (b * a)::s
    | a::b::s, DIV -> (b / a)::s
    | a::s, SIN -> (sin a)::s
    | a::s, COS -> (cos a)::s
    | a::s, LOG -> (log a)::s
    | a::s, EXP -> (exp a)::s
    | s, PUSH(x) -> x::s

intpInstr [5.; 5.; 5.;] MULT // [25.0; 5.0]

// 2.
let rec calc i s =
    match i, s with
    | [], r::_ -> r
    | i::is, s -> calc is (intpInstr s i)

let intProg i = calc i []

// 25 * 10 - 30
intProg [PUSH(25.); PUSH(10.); MULT; PUSH(30.); SUB] // 220.0

// 3.
let rec toProgram e x =
    match e with
    | Const(a) -> [PUSH(a)]
    | X -> [PUSH(x)]
    | Add(a,b) -> (toProgram a x) @ (toProgram b x) @ [ADD]
    | Sub(a,b) -> (toProgram a x) @ (toProgram b x) @ [SUB]
    | Mul(a,b) -> (toProgram a x) @ (toProgram b x) @ [MULT]
    | Div(a,b) -> (toProgram a x) @ (toProgram b x) @ [DIV]
    | Sin(a) -> (toProgram a x) @ [SIN]
    | Cos(a) -> (toProgram a x) @ [COS]
    | Log(a) -> (toProgram a x) @ [LOG]
    | Exp(a) -> (toProgram a x) @ [EXP]

let trans e x = intProg (toProgram e x)

// 25 * 10 - 30
let exp1 = Sub(Mul(Const 25., Const 10.), Const 30.)
toProgram exp1 0. // [PUSH(25.); PUSH(10.); MULT; PUSH(30.); SUB] 
trans exp1 0. // 220

// 25 * (10 - 30)
let exp2 = Mul(Const 25., Sub(Const 10., Const 30.))
toProgram exp2 0. // [PUSH 25.0; PUSH 10.0; PUSH 30.0; SUB; MULT]
trans exp2 0. // -500

// Exercise 3.9
// (HR 6.9)

// 1.
type Department = Department of string * Department list

// 2.
type Department2 = Department2 of string * float * Department2 list

// 3.
// The input to the function is not specified and as such it is assumed to be a list of departments

let rec nameIncomeAll = 
    function
    | [] -> []
    | Department2(name, income, [])::d -> (name, income)::(nameIncomeAll d)
    | Department2(name, income, subs)::d -> (name, income)::(nameIncomeAll subs) @ (nameIncomeAll d)

let rec nameIncomeAll2 ds =
    List.foldBack (fun (Department2(n,i,subs)) -> (@) ((n, i)::nameIncomeAll2 subs)) ds []

// [Zoo[Monkey; Giraffe]; Police]
nameIncomeAll [Department2("Zoo", 10., [Department2("Monkey", 1., []); Department2("Giraffe", 1., [])]);Department2("Police", 10., [])]
nameIncomeAll2 [Department2("Zoo", 10., [Department2("Monkey", 1., []); Department2("Giraffe", 1., [])]);Department2("Police", 10., [])]
// [("Zoo", 10.); ("Monkey", 1.); ("Giraffe", 1.); ("Police", 10.)]

// 4.
let rec totalIncome (Department2(_, income, subs)) = income + (List.sumBy totalIncome subs)

// 0[10[2; 3]; 13]
totalIncome (Department2("", 0., [Department2("", 10., [Department2("", 2., []); Department2("", 3., [])]);Department2("", 13., [])])) // 28

// 5.
// The input to the function is not specified and as such it is assumed to be a list of departments
let rec nameTotalIncomeAll = 
    function
    | [] -> []
    | (Department2(name, _, []) as dep)::d -> (name, totalIncome dep)::(nameTotalIncomeAll d)
    | (Department2(name, _, subs) as dep)::d -> (name, totalIncome dep)::(nameTotalIncomeAll subs) @ (nameTotalIncomeAll d)

let rec nameTotalIncomeAll2 ds =
    List.foldBack (fun (Department2(n,_,subs) as d) -> (@) ((n, totalIncome d)::nameIncomeAll2 subs)) ds []

nameTotalIncomeAll [Department2("Zoo", 10., [Department2("Monkey", 1., []); Department2("Giraffe", 1., [])]);Department2("Police", 10., [])]
nameTotalIncomeAll2 [Department2("Zoo", 10., [Department2("Monkey", 1., []); Department2("Giraffe", 1., [])]);Department2("Police", 10., [])]
// [("Zoo", 12); ("Monkey", 1); ("Girrafe", 1); ("Police", 10)]

// 6.

let rec formatInner (Department2(name, income, subs)) indent:string = 
    (String.replicate indent "    ") + name + " (" + string income + ")" + "\n" +
    (List.foldBack (fun d -> (+) (formatInner d (indent + 1))) subs "")

let rec sumStr f xs = List.foldBack (f >> (+)) xs ""
let rec formatInner2 (Department2(name, income, subs)) indent:string = 
    (String.replicate indent "    ") + name + " (" + string income + ")" + "\n" +
    (sumStr (fun d -> formatInner d (indent + 1)) subs)

let rec format d = formatInner d 0
let rec format2 d = formatInner2 d 0

let f = format (Department2("HQ", 0., [Department2("Zoo", 10., [Department2("Ape", 2., [Department2("Monkey Business", 13., []);Department2("Serious Business", 13., [])]);Department2("Giraffe", 3., [])]);Department2("Police", 13., [])]))
let f2 = format (Department2("HQ", 0., [Department2("Zoo", 10., [Department2("Ape", 2., [Department2("Monkey Business", 13., []);Department2("Serious Business", 13., [])]);Department2("Giraffe", 3., [])]);Department2("Police", 13., [])]))
printf "%s" f
printf "%s" f2