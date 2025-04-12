namespace RMGPA.Core



open System
open System.Collections.Generic

// ========================================
// Tissue Model
// ========================================
module Tissue =

    type Grid = Grid of int * int

    type Role =
        | Control
        | Memory
        | Sensor
        | Processor

    type Organelle =
        { Name: string
          Execute: Cell -> Cell }

    and Cell =
        { Id: string
          Role: Role
          Location: Grid
          Memory: Map<string, obj>
          Organelles: Organelle list }

    // Example organelles
    let processor =
        { Name = "Processor"
          Execute = fun cell ->
              let cycles =
                  match Map.tryFind "cpu_cycles" cell.Memory with
                  | Some(:? int as i) -> i + 1
                  | _ -> 1
              { cell with Memory = cell.Memory.Add("cpu_cycles", box cycles) } }

    let memory =
        { Name = "Memory"
          Execute = fun cell -> cell } // placeholder logic

    let signal =
        { Name = "Signal"
          Execute = fun cell -> cell } // placeholder logic

// ========================================
// Simulation Engine (used in both CLI & UI)
// ========================================
module SimulationEngine =

    open Tissue

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

    let step (cells: Cell list) =
        cells
        |> List.map (fun cell ->
            cell.Organelles
            |> List.fold (fun c o -> o.Execute c) cell)

    let memoryToChar cell =
        let cycles =
            match cell.Memory |> Map.tryFind "cpu_cycles" with
            | Some(:? int as i) -> i
            | _ -> 0

        match cycles with
        | 0 -> '·'
        | 1 | 2 -> '░'
        | 3 | 4 | 5 -> '▒'
        | 6 | 7 | 8 | 9 -> '▓'
        | _ -> '█'

    let renderGrid (cells: Cell list) : string =
        let grid =
            [ for y in 0..9 ->
                [ for x in 0..9 ->
                    cells
                    |> List.tryFind (fun c -> c.Location = Grid(x, y))
                    |> Option.map memoryToChar
                    |> Option.defaultValue '.' ]
                |> System.String.Concat ]
        String.Join("\n", grid)
