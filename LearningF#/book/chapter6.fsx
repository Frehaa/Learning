(* 6.1  Declare a function red of type Fexpr -> Fexpr to reduce expressions generated from the
differentiation program in Section 6.2. For example, sub-expressions of form Const 1.0 * e
can be reduced to e . (A solution is satisfactory if the expression becomes “nicer”. It is difficult
to design a reduce function so that all trivial sub-expressions are eliminated.) *)



(* 6.2 Postfix form is a particular representation of arithmetic expressions where each operator is
preceded by its operand(s), for example:
(x + 7.0) has postfix form x 7.0 +
(x + 7.0) ∗ (x − 5.0) has postfix form x 7.0 + x 5.0 − ∗
Declare an F# function with type Fexpr -> string computing the textual, postfix form of
expression trees from Section 6.2. *)

(*  6.3 Make a refined version of the toString function on Page 130 using the following conventions: 
    A subtrahend, factor or dividend must be in brackets if it is an addition or subtraction.
    A divisor must be in brackets if it is an addition, subtraction, multiplication or division. The
    argument of a function must be in brackets unless it is a constant or the variable x. (Hint: use a
    set of mutually recursive declarations.) *)
