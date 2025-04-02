module RMGPA.Core.Evaluator

open RMGPA.Core

let interpretGoal (cell: Cell) (goal: Goal) =
    match goal with
    | Maintain (metric, threshold) ->
        printfn "Cell %s maintaining %s >= %f" cell.Id metric threshold
    | Achieve (target, value) ->
        printfn "Cell %s trying to achieve %s = %A" cell.Id target value
