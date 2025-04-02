namespace RMGPA.Core

type Expr =
    | Add of int * int
    | Multiply of int * int

module Evaluator =
    let eval expr =
        match expr with
        | Add (a, b) -> a + b
        | Multiply (a, b) -> a * b
