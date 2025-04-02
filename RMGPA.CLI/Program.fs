open System
open RMGPA.Core
open RMGPA.Core.Tissue
open RMGPA.Core.Tissue.Exports

// Inline Evaluator stub (normally would live in Evaluator.fs)
module Evaluator =
    let interpretGoal (cell: Cell) (goal: obj) =
        printfn "Evaluating goal for cell %s with goal: %O" cell.Id goal
        cell

// Map Role to ASCII symbol
let roleToChar = function
    | Role.Control -> 'C'
    | Role.Processor -> 'P'
    | Role.Memory -> 'M'
    | Role.Sensor -> 'S'

// Generate 100 cells in a 10x10 grid
let generateGridCells () =
    [ for x in 0..9 do
        for y in 0..9 ->
            let loc = Grid(x, y)
            let role =
                if x = y then Role.Control
                elif (x + y) % 4 = 0 then Role.Memory
                elif (x + y) % 5 = 0 then Role.Sensor
                else Role.Processor

            { Id = Guid.NewGuid().ToString()
              Role = role
              Location = loc
              Memory = Map.empty
              Organelles = [ processor; memory; signal ] } ]

// Render the tissue as ASCII
let renderGrid cells =
    for y in 0..9 do
        for x in 0..9 do
            let roleChar =
                cells
                |> List.tryFind (fun c -> c.Location = Grid(x, y))
                |> Option.map (fun c -> roleToChar c.Role)
                |> Option.defaultValue '.'
            printf "%c " roleChar
        printfn ""

[<EntryPoint>]
let main _ =
    let cells = generateGridCells ()

    // Evaluate a simple goal for each cell
    cells
    |> List.iter (fun cell -> Evaluator.interpretGoal cell (box "Maintain energy") |> ignore)

    // Visualize as ASCII grid
    printfn "\nMorphogenetic Tissue Layout:\n"
    renderGrid cells

    0
