// Exercise 1. Extend the expression language and monadic evaluators with
// single-argument functions such as ABS(e1) which evaluates e1 and
// produces its absolute value.  Do this by adding a new case Prim1 of
// string * expr to the expr datatype.  Create suitable variants of all
// the monadic evaluators; you should not have to change the monad
// definitions (OptionBuilder, SetBuilder, TraceBuilder) at all.
// Abstract out the action of ABS on its argument in new auxiliary
// functions opEvalOpt1, opEvalSet1 and opEvalTrace1 similar to the
// existing functions opEvalOpt, opEvalSet and opEvalTrace for
// two-argument primitives.  Try the new evaluators on eg these
// expressions:

type expr =
    | CstI of int
    | Prim of string * expr * expr
    | Prim1 of string * expr

let expr10 = Prim1("ABS", Prim("+", CstI(7), Prim("*", CstI(-9), CstI(10))))
let expr11 = Prim1("ABS", Prim("+", CstI(7), Prim("/", CstI(9), CstI(0))))
let expr12 = Prim("+", CstI(7), Prim("choose", Prim1("ABS", CstI(-9)), CstI(10)))

// ------------------------------------------------------------

// Evaluator that may fail, return type: int option

type OptionBuilder() =
    member this.Bind(x, f) =
        match x with
        | None   -> None
        | Some v -> f v
    member this.Return x = Some x
    member this.ReturnFrom x = x
let optionM = OptionBuilder();;

let opEvalOpt op v1 v2 : int option =
    match op with
    | "+" -> Some(v1 + v2)
    | "*" -> Some(v1 * v2)
    | "/" -> if v2 = 0 then None else Some(v1 / v2)

let opEvalOpt1 op v : int option =
    match op with
    | "ABS" -> Some (abs v)

let rec optionEval3 e : int option =
    match e with
    | CstI i -> optionM { return i }
    | Prim(op, e1, e2) ->
        optionM { let! v1 = optionEval3 e1
                  let! v2 = optionEval3 e2
                  return! opEvalOpt op v1 v2 }
    | Prim1(op, e) -> 
        optionM { let! v = optionEval3 e
                  return! opEvalOpt1 op v}

optionEval3 expr10
optionEval3 expr11


// ------------------------------------------------------------                

// Evaluator that returns a set of results, return type: int Set

type SetBuilder() =
    member this.Bind(x, f) =
        Set.unionMany (Set.map f x)
    member this.Return x = Set [x]
    member this.ReturnFrom x = x
let setM = SetBuilder();;

let opEvalSet op v1 v2 : int Set =
    match op with
    | "+" -> Set [v1 + v2]
    | "*" -> Set [v1 * v2]
    | "/" -> if v2 = 0 then Set.empty else Set [v1 / v2]
    | "choose" -> Set [v1; v2]

let opEvalSet1 op v = 
    match op with
    | "ABS" -> Set [abs v]

let rec setEval3 e : int Set =
    match e with
    | CstI i -> setM { return i }
    | Prim(op, e1, e2) ->
        setM { let! v1 = setEval3 e1
               let! v2 = setEval3 e2
               return! opEvalSet op v1 v2 }
    | Prim1(op, e) ->               
        setM { let! v = setEval3 e
               return! opEvalSet1 op v}


setEval3 expr10
setEval3 expr11
setEval3 expr12


// ------------------------------------------------------------

// Evaluator that records sequence of operators used,
// return type: int trace
let random = new System.Random()

type 'a trace = string list * 'a

let opEvalTrace op v1 v2 : int trace =
    match op with
    | "+" -> (["+"], v1 + v2)
    | "*" -> (["*"], v1 * v2)
    | "/" -> (["/"], v1 / v2)
    | "choose" -> (["choose"], if random.NextDouble() > 0.5 then v1 else v2)

let opEvalTrace1 op v = 
    match op with
    | "ABS" -> (["ABS"], abs v)


type TraceBuilder() =
    member this.Bind(x, f) =
        let (trace1, v) = x
        let (trace2, res) = f v
        (trace1 @ trace2, res)
    member this.Return x = ([], x)
    member this.ReturnFrom x = x
 
let traceM = TraceBuilder();;

let rec traceEval3 e : int trace =
    match e with
    | CstI i -> traceM { return i }
    | Prim(op, e1, e2) ->
        traceM { let! v1 = traceEval3 e1
                 let! v2 = traceEval3 e2
                 return! opEvalTrace op v1 v2 }
    | Prim1(op, e) -> 
        traceM { let! v = traceEval3 e
                 return! opEvalTrace1 op v}

traceEval3 expr10
traceEval3 expr12





// Exercise 2. Extend the expression language and the monadic evaluators
// with a three-argument function such as +(e1, e2, e3) that is basically
// two applications of "+", as in, +(+(e1,e2),e3).  Do this by adding a
// new constructor Prim3 of string * expr * expr * expr to the expr type.
// You may alternatively add a more general facility for functions with
// n>=1 arguments, such as SUM(e1, ..., en), adding a suitable
// constructor to the expr type.
// Implement evaluation of such three-argument (or multi-argument)
// constructs in the monadic evaluators.  

type expr =
    | CstI of int
    | Prim of string * expr * expr
    | Prim1 of string * expr
    | Prim3 of string * expr * expr * expr

let expr3 = Prim1("ABS", Prim3("+", CstI(-12), CstI(5), CstI(-3)))

// ------------------------------------------------------------

// Evaluator that may fail, return type: int option

type OptionBuilder() =
    member this.Bind(x, f) =
        match x with
        | None   -> None
        | Some v -> f v
    member this.Return x = Some x
    member this.ReturnFrom x = x
let optionM = OptionBuilder();;

let opEvalOpt op v1 v2 : int option =
    match op with
    | "+" -> Some(v1 + v2)
    | "*" -> Some(v1 * v2)
    | "/" -> if v2 = 0 then None else Some(v1 / v2)

let opEvalOpt1 op v : int option =
    match op with
    | "ABS" -> Some (abs v)

let rec optionEval3 e : int option =
    match e with
    | CstI i -> optionM { return i }
    | Prim(op, e1, e2) ->
        optionM { let! v1 = optionEval3 e1
                  let! v2 = optionEval3 e2
                  return! opEvalOpt op v1 v2 }
    | Prim1(op, e) -> 
        optionM { let! v = optionEval3 e
                  return! opEvalOpt1 op v}
    | Prim3(op, e1, e2, e3) ->
        optionM { let! v1 = optionEval3 e1
                  let! v2 = optionEval3 e2
                  let! v3 = optionEval3 e3
                  let! t = opEvalOpt op v1 v2
                  return! opEvalOpt op t v3 }                  

optionEval3 expr3


// ------------------------------------------------------------                

// Evaluator that returns a set of results, return type: int Set

type SetBuilder() =
    member this.Bind(x, f) =
        Set.unionMany (Set.map f x)
    member this.Return x = Set [x]
    member this.ReturnFrom x = x
let setM = SetBuilder();;

let opEvalSet op v1 v2 : int Set =
    match op with
    | "+" -> Set [v1 + v2]
    | "*" -> Set [v1 * v2]
    | "/" -> if v2 = 0 then Set.empty else Set [v1 / v2]
    | "choose" -> Set [v1; v2]

let opEvalSet1 op v = 
    match op with
    | "ABS" -> Set [abs v]

let rec setEval3 e : int Set =
    match e with
    | CstI i -> setM { return i }
    | Prim(op, e1, e2) ->
        setM { let! v1 = setEval3 e1
               let! v2 = setEval3 e2
               return! opEvalSet op v1 v2 }
    | Prim1(op, e) ->               
        setM { let! v = setEval3 e
               return! opEvalSet1 op v}
    | Prim3(op, e1, e2, e3) ->
        setM { let! v1 = setEval3 e1
               let! v2 = setEval3 e2
               let! v3 = setEval3 e3
               let! t = opEvalSet op v1 v2
               return! opEvalSet op t v3 }    

let expr4 = Prim1("ABS", Prim3("+", Prim("choose", CstI(-12), CstI(-100)), CstI(5), CstI(-3)))

setEval3 expr10
setEval3 expr11
setEval3 expr12
setEval3 expr3
setEval3 expr4

// ------------------------------------------------------------

// Evaluator that records sequence of operators used,
// return type: int trace
let random = new System.Random()

type 'a trace = string list * 'a

let opEvalTrace op v1 v2 : int trace =
    match op with
    | "+" -> (["+"], v1 + v2)
    | "*" -> (["*"], v1 * v2)
    | "/" -> (["/"], v1 / v2)
    | "choose" -> (["choose"], if random.NextDouble() > 0.5 then v1 else v2)

let opEvalTrace1 op v = 
    match op with
    | "ABS" -> (["ABS"], abs v)


type TraceBuilder() =
    member this.Bind(x, f) =
        let (trace1, v) = x
        let (trace2, res) = f v
        (trace1 @ trace2, res)
    member this.Return x = ([], x)
    member this.ReturnFrom x = x
 
let traceM = TraceBuilder();;

let rec traceEval3 e : int trace =
    match e with
    | CstI i -> traceM { return i }
    | Prim(op, e1, e2) ->
        traceM { let! v1 = traceEval3 e1
                 let! v2 = traceEval3 e2
                 return! opEvalTrace op v1 v2 }
    | Prim1(op, e) -> 
        traceM { let! v = traceEval3 e
                 return! opEvalTrace1 op v}
    | Prim3(op, e1, e2, e3) ->
        traceM { let! v1 = traceEval3 e1
                 let! v2 = traceEval3 e2
                 let! v3 = traceEval3 e3
                 let! t = opEvalTrace op v1 v2
                 return! opEvalTrace op t v3 }    

traceEval3 expr10
traceEval3 expr12
traceEval3 expr3
traceEval3 expr4


// Exercise 3. Create a new family of evaluation functions
// optionTraceEval. These evaluators should combine the effect of the
// original optional evaluator (optionEval) and the original tracing
// evaluator (traceEval).
// This can be done in several ways, for instance corresponding to (A)
// return type int trace option, for an evaluator that returns no trace
// if a computation fails; or (B) the result type int option trace, for
// an evaluator that returns a partial trace up until some computation
// (eg division by zero) fails.

type expr =
    | CstI of int
    | Prim of string * expr * expr

let random = new System.Random()

type 'a trace = string list * 'a

let expr1 = Prim("+", CstI(5), CstI(3))
let expr2 = Prim("/", CstI(5), CstI(0))
let expr3 = Prim("-", expr1, Prim("*", CstI(10), CstI(2)))

// 3.1: Make both a standard explicit version of (A) and a monadic
// version.  You need to create a new monad OptionTraceABuilder, among
// other things.

type 'a OptionTrace = 'a trace Option

let opEval op v1 v2 =
    match op with
    | "+" -> Some (["+"], v1 + v2)
    | "-" -> Some (["-"], v1 + v2)
    | "*" -> Some (["*"], v1 + v2)
    | "/" -> 
        if v2 = 0 then None 
        else Some (["/"], v1 / v2)

let rec eval = function
| CstI v -> Some ([], v)
| Prim (op, e1, e2) -> 
    match (eval e1, eval e2) with 
    | (Some (l1, v1), Some (l2, v2))  ->         
            match opEval op v1 v2 with
            | None -> None
            | Some (l3, v3) -> 
                Some (l1 @ l2 @ l3, v3)
    | _ -> None        

eval expr1 // Some (["+"], 8)
eval expr2 // None
eval expr3 // Some (["+"; "*"; "-"], 20)

type OptionTraceBuilder() = 
    member this.Bind (x, f) = 
        match x with
        | None -> None
        | Some (trace1, v) -> 
            let (trace2, res) = f v
            Some (trace1 @ trace2, res)
    member this.Return x = Some ([], x)           
    member this.ReturnFrom x = x

let OptTraceB = OptionTraceBuilder()

let opEval op v1 v2 =
    match op with
    | "+" -> Some (["+"], v1 + v2)
    | "-" -> Some (["-"], v1 + v2)
    | "*" -> Some (["*"], v1 + v2)
    | "/" -> 
        if v2 = 0 then None 
        else Some (["/"], v1 / v2)

let rec eval e : string list * int option =
    match e with
    | CstI i -> OptTraceB { return i }

eval (CstI 5)

eval expr1 // Some (["+"], 8)
eval expr2 // None
eval expr3 // Some (["+"; "*"; "-"], 20)


// 3.2: Make both a standard explicit version of (B) and a monadic
// version.  You need to create a new monad OptionTraceBBuilder, among
// other things.

type 'a optionTrace = 'a Option trace

let opEval op v1 v2 =
    match op with
    | "+" -> (["+"], Some (v1 + v2))
    | "-" -> (["-"], Some (v1 - v2))
    | "*" -> (["*"], Some (v1 * v2))
    | "/" -> 
        if v2 = 0 then (["/"], None)
        else (["/"], Some (v1 / v2))

    
let rec eval = function
| CstI v -> ([],  Some v)
| Prim (op, e1, e2) -> 
    let (l1, s1) = eval e1
    let (l2, s2) = eval e2
    if s1 = None || s2 = None then (l1 @ l2, None)
    else 
        let v1 = Option.get s1
        let v2 = Option.get s2
        let (l3, v3) = opEval op v1 v2
        (l1 @ l2 @ l3, v3)

eval expr1 // Some (["+"], 8)
eval expr2 // None
eval expr3 // Some (["+"; "*"; "-"], 20)    
     